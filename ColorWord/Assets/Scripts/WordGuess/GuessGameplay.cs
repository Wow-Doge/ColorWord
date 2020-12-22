using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Networking;
using SimpleJSON;



public class GuessGameplay : SingletonComponent<GuessGameplay>
{
    public GameObject guessGameplay;
    public GameObject letterPrefab;

    GameObject imageField;

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

    [HideInInspector]
    public List<DataList> dataList = new List<DataList>();

    //[HideInInspector]
    public List<ListOfChallenge> challengeListGoals = new List<ListOfChallenge>();

    public GameObject challengeList;
    [SerializeField]
    private GameObject uIChallenge;

    [SerializeField] private GameObject uITimeMode;
    [SerializeField] private GameObject uILockMode;
    [SerializeField] private GameObject totalClickGO;
    [SerializeField] private GameObject clockGO;
    [SerializeField] private GameObject uIPopup;

    private int totalClick;
    private int maxClick = 20;

    private float currentTime;
    private bool isTimeout;
    private float startingTime = 50f;

    private event EventHandler LockEvent;
    private event EventHandler TimeEvent;

    WordAPI wordAPI;
    void Start()
    {
        answerField = guessGameplay.transform.GetChild(0).gameObject.transform;
        imageField = guessGameplay.transform.GetChild(1).gameObject;
        currentLevelObject = guessGameplay.transform.GetChild(5).GetChild(0).gameObject;
        coinField = guessGameplay.transform.GetChild(4).gameObject;
        bonusField = guessGameplay.transform.GetChild(5).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
        coinText = coinField.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        bonusText = bonusField.transform.GetComponent<TextMeshProUGUI>();
        uIComplete = uICompleteScreen.transform.GetChild(1).gameObject;
        uIDescription = uICompleteScreen.transform.GetChild(2).gameObject;
        wordAPI = GetComponent<WordAPI>();
    }

    public void CreateAnswerField()
    {
        StringBuilder sb = new StringBuilder(userInput, 20);

        //delete answer text object from previous level
        if (answerField.childCount > 0)
        {
            sb.Remove(0, userInput.Length);
            foreach (Transform childObject in answerField.transform)
            {
                Destroy(childObject.gameObject);
            }
        }
        //add '_' to userInput for each letter in answer
        //Example: answer = "CAT" --> userInput: "___"
        for (int i = 0; i < answer.Length; i++)
        {
            sb.Append(placeholder);
            userInput = sb.ToString();
            Instantiate(letterPrefab, new Vector3(answerField.position.x, answerField.position.y, answerField.position.z), Quaternion.identity, answerField);
            TextMeshProUGUI textLetter = answerField.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            textLetter.text = placeholder.ToString();
        }
    }

    //when player press a button
    public void InputSystem(Button button)
    {
        char letter = button.GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[0];
        UpdateAnswer(letter);
        LockEvent?.Invoke(this, EventArgs.Empty);
        if (!userInput.Contains(placeholder))
        {
            if (!CheckWinCondition())
            {
                ResetUserInput();
            }
            else
            {
                StartCoroutine(ShowTextWhenCorrect());
                StartCoroutine(ShowCompleteScreen());
            }
        }
    }

    public void ResetUserInput()
    {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < userInput.Length; i++)
        {
            userInputArray[i] = placeholder;
            TextMeshProUGUI text = answerField.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = userInputArray[i].ToString();
        }
        userInput = new string(userInputArray);
    }

    public void GetHint()
    {
        int index = UnityEngine.Random.Range(0, answer.Length);
        char c = answer[index];

        char[] userInputArray = userInput.ToCharArray();
        userInputArray[index] = c;

        TextMeshProUGUI text = answerField.GetChild(index).GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = userInputArray[index].ToString();
        text.color = Color.blue;

        userInput = new string(userInputArray);
    }

    //add a letter in the answer field 
    public void UpdateAnswer(char letter)
    {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++)
        {
            if (userInputArray[i] != placeholder)
            {
                continue;
            }
            if (userInputArray[i] == placeholder)
            {
                userInputArray[i] = letter;
                TextMeshProUGUI text = answerField.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
                text.text = letter.ToString();
                break;
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
        uICompleteScreen.SetActive(true);
        Image image = uICompleteScreen.gameObject.transform.GetChild(0).gameObject.transform.GetComponent<Image>();
        StartCoroutine(UIManager.Instance.FadeIn(image));
        yield return new WaitForSeconds(1f);
        RectTransform rect = uIComplete.gameObject.GetComponent<RectTransform>();
        StartCoroutine(UIManager.Instance.ScaleUp(rect));
        yield return new WaitForSeconds(2f);
        uIDescription.SetActive(true);
        StartCoroutine(wordAPI.GetAPI(answer));
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
    public void IncreaseCategoryLevelNumber()
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

    public void IncreaseChallengeGoal()
    {
        for (int i = 0; i < challengeList.transform.childCount; i++)
        {
            ChallengeListItem challengeListItem = challengeList.transform.GetChild(i).gameObject.transform.GetComponent<ChallengeListItem>();
            if (challengeListItem.challengeName == "Level")
            {
                challengeListItem.curGoal++;
            }
            break;
        }
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
        IncreaseCategoryLevelNumber();
        IncreaseChallengeGoal();
        GetDataList();
        GetChallengeList();
        SaveLoadSystem.Save();

        LockEvent -= ClickCount;
        TimeEvent -= TimeCount;
    }

    public void ReturnToUILevel()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        uILevel.ShowLevel();
        uILevel.SetupLevel();
        Image image = uICompleteScreen.gameObject.transform.GetChild(0).gameObject.transform.GetComponent<Image>();
        UIManager.Instance.FadeOut(image);

        totalClickGO.SetActive(false);


        clockGO.SetActive(false);


        uIDescription.SetActive(false);
        uICompleteScreen.SetActive(false);
        guessGameplay.SetActive(false);
    }

    public void BonusCoinsStart(bool isTime, bool isLock)
    {
        if (isTime || isLock)
        {
            bonusCoins = 50;
            bonusText.text = "Bonus: " + bonusCoins + "$";
        }
        else
        {
            bonusCoins = 25;
            bonusText.text = "Bonus: " + bonusCoins + "$";
        }

    }
    public void TimeMode()
    {
        //Start mode
        Image image = uITimeMode.transform.GetComponent<Image>();
        StartCoroutine(UIManager.Instance.FadeIn(image));
        clockGO.SetActive(true);
        isTimeout = false;
        currentTime = startingTime;
        TimeEvent += TimeCount;
    }

    private void TimeCount(object sender, EventArgs e)
    {
        currentTime -= 1 * Time.deltaTime;
        TextMeshProUGUI text = clockGO.transform.GetComponent<TextMeshProUGUI>();
        text.text = "Time: " + currentTime.ToString("0");
        if (currentTime < 0)
        {
            isTimeout = true;
            uIPopup.SetActive(true);
            TimeEvent -= TimeCount;
        }
    }

    public void LockMode()
    {
        //Start mode    
        Image image = uILockMode.transform.GetComponent<Image>();
        StartCoroutine(UIManager.Instance.FadeIn(image));
        totalClickGO.SetActive(true);
        LockEvent += ClickCount;
    }

    private void ClickCount(object sender, EventArgs e)
    {
        totalClick += 1;
        totalClickGO.transform.GetComponent<TextMeshProUGUI>().text = "Total click: " + totalClick.ToString() + "/" + maxClick.ToString();
        if (totalClick >= 20)
        {
            uIPopup.SetActive(true);
            LockEvent -= ClickCount;
        }
    }

    public void Update()
    {
        TimeEvent?.Invoke(this, EventArgs.Empty);
    }

    //get the information of level
    public void StartLevel(string answer, string levelQuestion, int levelIndex, LevelListItem.Type type, Sprite sprite, string description, bool isTimeMode, bool isLockMode)
    {
        this.answer = answer;
        //questionField.transform.gameObject.GetComponent<TextMeshProUGUI>().text = levelQuestion;
        this.activeLevelIndex = levelIndex;
        this.type = type;
        this.sprite = sprite;
        this.description = description;

        totalClick = 0;

        if (isTimeMode)
        {
            TimeMode();
        }

        if (isLockMode)
        {
            LockMode();
        }

        CreateAnswerField();
        currentLevelObject.transform.gameObject.GetComponent<TextMeshProUGUI>().text = "LEVEL " + levelIndex.ToString();
        imageField.gameObject.GetComponent<Image>().sprite = sprite;
        coinText.text = CoinNumber.ToString();
        BonusCoinsStart(isTimeMode, isLockMode);
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
            if (dataList.Count == 0)
            {
                dataList.Add(new DataList(categoryListItem));
            }
            if (dataList.Count > 0)
            {
                //Check all categoryName in playerDataList
                if (dataList.Any(category => category.categoryName == categoryListItem.categoryName))
                {
                    //get numbers of completed level
                    foreach (var data in dataList)
                    {
                        if (data.categoryName == categoryListItem.categoryName)
                        {
                            data.completedLevel = categoryListItem.NumOfActiveLevel;
                        }
                    }
                }
                else if (dataList.Any(category => category.categoryName != categoryListItem.categoryName))
                {
                    dataList.Add(new DataList(categoryListItem));
                }
            }
        }
    }

    public void GetChallengeList()
    {
        for (int i = 0; i < challengeList.transform.childCount; i++)
        {
            ChallengeListItem challengeListItem = challengeList.transform.GetChild(i).gameObject.GetComponent<ChallengeListItem>();
            if (challengeListGoals.Count == 0)
            {
                challengeListGoals.Add(new ListOfChallenge(challengeListItem));
            }
            if (challengeListGoals.Count > 0)
            {
                if (challengeListGoals.Any(challenge => challenge.name == challengeListItem.challengeName))
                {
                    foreach (var data in challengeListGoals)
                    {
                        if (data.name == challengeListItem.challengeName)
                        {
                            data.curGoal = challengeListItem.curGoal;
                        }
                    }
                }
                else if (challengeListGoals.Any(challenge => challenge.name != challengeListItem.challengeName))
                {
                    challengeListGoals.Add(new ListOfChallenge(challengeListItem));
                }
            }
            challengeListItem.ChallengeUIUpdate();
        }
    }

    public void LoadData()
    {
        PlayerData dataList = SaveLoadSystem.Load();
        if (dataList != null)
        {
            for (int i = 0; i < categoryList.gameObject.transform.childCount; i++)
            {
                CategoryListItem categoryListItem = categoryList.gameObject.transform.GetChild(i).gameObject.transform.GetComponent<CategoryListItem>();
                foreach (var data in dataList.dataList)
                {
                    if (data.categoryName == categoryListItem.categoryName)
                    {
                        categoryListItem.NumOfActiveLevel = data.completedLevel;
                    }
                }
            }
            CoinNumber = dataList.coins;
        }
    }
}



