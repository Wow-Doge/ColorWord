using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System;
using System.IO;
using DG.Tweening;

public class GuessGameplay : SingletonComponent<GuessGameplay>
{
    public GameObject guessGameplay;
    public GameObject letterPrefab;

    GameObject questionField;

    public GameObject uICompleteScreen;
    GameObject uIComplete;
    GameObject uIDescription;


    private const char placeholder = '_';

    private string answer;
    private string userInput;

    Transform answerField;

    public string activeCategoryInfo;
    int activeLevelIndex;
    Sprite sprite;
    string description;
    [SerializeField]
    private LevelListItem.Type type;

    GameObject currentLevelObject;

    public GameObject categoryList;

    GameObject coinField;
    TextMeshProUGUI coinText;
    public int CoinNumber { get; set; } = 0;
    private int bonusCoins;

    GameObject bonusField;
    TextMeshProUGUI bonusText;

    public GameObject congratulationText;

    public List<PlayerData> playerDataList = new List<PlayerData>();


    void Start()
    {
        answerField = guessGameplay.transform.GetChild(0).gameObject.transform;
        questionField = guessGameplay.transform.GetChild(1).gameObject;
        currentLevelObject = guessGameplay.transform.GetChild(5).GetChild(0).gameObject;
        coinField = guessGameplay.transform.GetChild(4).gameObject;
        bonusField = guessGameplay.transform.GetChild(6).gameObject;
        coinText = coinField.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        bonusText = bonusField.transform.GetComponent<TextMeshProUGUI>();
        uIComplete = uICompleteScreen.transform.GetChild(1).gameObject;
        uIDescription = uICompleteScreen.transform.GetChild(2).gameObject;
    }

    public void CreateAnswerField()
    {
        StringBuilder sb = new StringBuilder(userInput, 20);

        //delete child from previous level
        if (answerField.childCount > 0)
        {
            sb.Remove(0, userInput.Length);
            foreach (Transform childObject in answerField.transform)
            {
                Destroy(childObject.gameObject);
            }
        }

        for (int i = 0; i < answer.Length; i++)
        {
            sb.Append(placeholder);
            userInput = sb.ToString();
            Instantiate(letterPrefab, new Vector3(answerField.position.x, answerField.position.y, answerField.position.z), Quaternion.identity, answerField);
            TextMeshProUGUI textLetter = answerField.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            textLetter.text = placeholder.ToString();
        }
    }

    public void InputSystem(Button button)
    {
        char letter = button.GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[0];
        if (answer.Contains(letter))
        {
            UpdateAnswer(letter);
            StartCoroutine(ShowTextWhenCorrect());
            //if winning
            if (CheckWinCondition())
            {
                StartCoroutine(ShowCompleteScreen());
            }
        }
        else
        {
            Debug.Log("wrong letter");

            if (bonusCoins > 5)
            {
                bonusCoins -= 5;
            }
            else
            {
                bonusCoins = 0;
            }
            bonusText.text = "Bonus: " + bonusCoins + "$";
        }
    }

    public void GetHint()
    {
        if (CoinNumber >= 5)
        {
            StringBuilder sb = new StringBuilder();
            char[] firstArray = answer.ToCharArray();
            char[] secondArray = userInput.ToCharArray();

            var differentChars = firstArray.Except(secondArray);
            foreach (char c in differentChars)
            {
                sb.Append(c);
            }

            char ch = sb[UnityEngine.Random.Range(0, sb.Length)];
            UpdateAnswer(ch);
            if (CheckWinCondition())
            {
                StartCoroutine(ShowCompleteScreen());
                //Do something to congratulate player;
            }
            CoinChange(-5);
        }
        else
        {
            Debug.Log("Not enough coin");
            //Will add some popup later
        }
    }

    //update the answer to the game
    public void UpdateAnswer(char letter)
    {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++)
        {
            if (userInputArray[i] != placeholder)
            {
                continue;
            }
            if (answer[i] == letter)
            {
                userInputArray[i] = letter;
                TextMeshProUGUI text = answerField.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
                text.text = letter.ToString();
            }
        }
        userInput = new string(userInputArray);
    }

    //check if userInput match the answer and return bool type
    public bool CheckWinCondition()
    {
        return answer.Equals(userInput);
    }

    IEnumerator ShowCompleteScreen()
    {
        yield return new WaitForSeconds(1f);
        uICompleteScreen.SetActive(true);
        uIComplete.SetActive(true);
        yield return new WaitForSeconds(1f);
        uIDescription.SetActive(true);
        uIComplete.SetActive(false);
        GetImageAndDescription();
        LevelComplete();
    }

    public void GetImageAndDescription()
    {
        Image image = uIDescription.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        image.sprite = sprite;
        TextMeshProUGUI descriptionText = uIDescription.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        descriptionText.text = description;
    }

    IEnumerator ShowTextWhenCorrect()
    {
        congratulationText.SetActive(true);
        yield return new WaitForSeconds(1f);
        congratulationText.SetActive(false);
    }

    //increase numbers of active level in category
    public void GetCategoryLevelNumber()
    {
        for (int i = 0; i < categoryList.transform.childCount; i++)
        {
            CategoryListItem categoryListItem = categoryList.transform.GetChild(i).GetComponent<CategoryListItem>();
            if (activeCategoryInfo == categoryListItem.categoryName)
            {
                if (type == LevelListItem.Type.Normal)
                {
                    categoryListItem.NumOfActiveLevel++;
                }
            }
        }
    }    

    //change player coins
    public void CoinChange(int num)
    {
        CoinNumber += num;
        coinText.text = CoinNumber.ToString();
    }


    //pass the info to UILevel, UICategory and save the game
    public void LevelComplete()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        if (type == LevelListItem.Type.Normal)
        {
            CoinChange(bonusCoins);
            uILevel.numbersOfActiveLevel++;
        }
        GetCategoryLevelNumber();
        GetDataList();
        Save();
    }

    public void ReturnToUILevel()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        uILevel.ShowLevel();
        uILevel.SetupLevel();
        uIDescription.SetActive(false);
        uICompleteScreen.SetActive(false);
        guessGameplay.SetActive(false);
    }

    public void BonusCoinsStart()
    {
        bonusCoins = 25;
        bonusText.text = "Bonus: " + bonusCoins + "$";
    }

    //get the information of level
    public void StartLevel(string answer, string levelQuestion, int levelIndex, LevelListItem.Type type, Sprite sprite, string description)
    {
        this.answer = answer;
        questionField.transform.gameObject.GetComponent<TextMeshProUGUI>().text = levelQuestion;
        this.activeLevelIndex = levelIndex;
        this.type = type;
        this.sprite = sprite;
        this.description = description;

        CreateAnswerField();
        currentLevelObject.transform.gameObject.GetComponent<TextMeshProUGUI>().text = "LEVEL " + levelIndex.ToString();
        coinText.text = CoinNumber.ToString();
        BonusCoinsStart();
    }

    public void ResetProgress()
    {
        File.Delete(Application.dataPath + "/text.txt");
    }

    public void GetDataList()
    {
        for (int i = 0; i < categoryList.gameObject.transform.childCount; i++)
        {
            CategoryListItem categoryListItem = categoryList.gameObject.transform.GetChild(i).gameObject.transform.GetComponent<CategoryListItem>();
            if (playerDataList.Count == 0)
            {
                playerDataList.Add(new PlayerData(categoryListItem));
            }
            if (playerDataList.Count > 0)
            {
                //Check all categoryName in playerDataList
                if (playerDataList.Any(category => category.categoryName == categoryListItem.categoryName))
                {
                    //get numbers of completed level
                    foreach (var data in playerDataList)
                    {
                        if (data.categoryName == categoryListItem.categoryName)
                        {
                            data.completedLevel = categoryListItem.NumOfActiveLevel;
                        }
                    }
                }
                else if (playerDataList.Any(category => category.categoryName != categoryListItem.categoryName))
                {
                    playerDataList.Add(new PlayerData(categoryListItem));
                }
            }
        }
    }
    public void Save()
    {
        DataList dataList = new DataList();
        Debug.Log(JsonUtility.ToJson(dataList));
        string path = Application.dataPath + "/text.txt";
        File.WriteAllText(path, JsonUtility.ToJson(dataList));
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/text.txt"))
        {
            string loadPath = File.ReadAllText(Application.dataPath + "/text.txt");
            DataList dataList = JsonUtility.FromJson<DataList>(loadPath);
            if (dataList != null)
            {
                for (int i = 0; i < categoryList.gameObject.transform.childCount; i++)
                {
                    CategoryListItem categoryListItem = categoryList.gameObject.transform.GetChild(i).gameObject.transform.GetComponent<CategoryListItem>();
                    foreach (var data in dataList.playerDatas)
                    {
                        if (data.categoryName == categoryListItem.categoryName)
                        {
                            categoryListItem.NumOfActiveLevel = data.completedLevel;
                        }
                    }
                }
                CoinNumber = dataList.coins;
            }
            else
            {
                Debug.Log("data is empty");
            }
        }
        else
        {
            //Create a text file and add path
            File.Create(Application.dataPath + "/text.txt");
        }
    }
}
[System.Serializable]
public class DataList
{
    public List<PlayerData> playerDatas = GuessGameplay.Instance.playerDataList;
    public int coins = GuessGameplay.Instance.CoinNumber;
}


