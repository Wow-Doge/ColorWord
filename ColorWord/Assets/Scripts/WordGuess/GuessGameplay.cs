using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System;

public class GuessGameplay : SingletonComponent<GuessGameplay>
{
    public GameObject guessGameplay;
    public GameObject letterPrefab;

    public GameObject questionField;

    public GameObject uICompleteScreen;
    public GameObject uIFailScreen;

    public string answer;

    private const char placeholder = '_';

    private string userInput;

    Transform answerField;

    public string activeCategoryInfo;
    public int activeLevelIndex;

    public GameObject currentLevelObject;

    public GameObject categoryList;

    public GameObject coinField;
    TextMeshProUGUI coinText;
    private int CoinNumber { get; set; } = 0;

    public GameObject errorField;
    TextMeshProUGUI errorText;
    private int curErrorNumber;
    private int totalErrorNumber;

    void Start()
    {
        answerField = guessGameplay.transform.GetChild(0).gameObject.transform;
        coinText = coinField.gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>();
        errorText = errorField.transform.GetComponent<TextMeshProUGUI>();
    }

    public void DisplayGame()
    {
        guessGameplay.SetActive(true);
    }

    public void CreateAnswerField()
    {
        StringBuilder sb = new StringBuilder(userInput, 20);
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
            TextMeshProUGUI textLetter = answerField.GetComponentInChildren<TextMeshProUGUI>();
            textLetter.text = placeholder.ToString();
        }
    }

    public void SubmitGuessLetter(Button button)
    {
        char letter = button.GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[0];
        if (answer.Contains(letter))
        {
            UpdateAnswer(letter);
            if (CheckWinCondition())
            {
                StopCoroutine(ShowCompleteScreen());
                Debug.Log("Congratulation");
                StartCoroutine(ShowCompleteScreen());
                //Do something to congratulate player;
            }
        }
        else
        {
            Debug.Log("wrong letter");
            //Punish player when they press wrong letter button;
            curErrorNumber++;
            errorText.text = "Error: " + curErrorNumber + " / " + totalErrorNumber;
            if (curErrorNumber >= totalErrorNumber)
            {
                StopCoroutine(ShowFailScreen());
                Debug.Log("Failed");
                StartCoroutine(ShowFailScreen());
            }
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
                StopAllCoroutines();
                Debug.Log("Congratulation");
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
                TextMeshProUGUI text = answerField.GetChild(i).gameObject.transform.GetChild(0).transform.gameObject.GetComponent<TextMeshProUGUI>();
                text.text = letter.ToString();
            }
        }
        userInput = new string(userInputArray);
    }

    public bool CheckWinCondition()
    {
        return answer.Equals(userInput);
    }

    IEnumerator ShowCompleteScreen()
    {
        CoinChange(25);
        yield return new WaitForSeconds(1f);
        uICompleteScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        uICompleteScreen.SetActive(false);
        ReturnToUILevel();
    }

    IEnumerator ShowFailScreen()
    {
        yield return new WaitForSeconds(1f);
        uIFailScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        uIFailScreen.SetActive(false);
        ReturnToUILevelFail();

    }
    public void GetCategoryLevelNumber()
    {
        for (int i = 0; i < categoryList.transform.childCount; i++)
        {
            CategoryListItem categoryListItem = categoryList.gameObject.transform.GetChild(i).gameObject.transform.GetComponent<CategoryListItem>();
            if (activeCategoryInfo == categoryListItem.categoryName)
            {
                categoryListItem.NumOfActiveLevel++;
            }
        }
    }    

    public void CoinChange(int num)
    {
        CoinNumber += num;
        coinText.text = CoinNumber.ToString();
    }

    public void ReturnToUILevel()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        GetCategoryLevelNumber();
        uILevel.numbersOfActiveLevel++;
        uILevel.ShowLevel();
        uILevel.DisplayLevel();
        guessGameplay.SetActive(false);
    }

    public void ReturnToUILevelFail()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        GetCategoryLevelNumber();
        uILevel.ShowLevel();
        uILevel.DisplayLevel();
        guessGameplay.SetActive(false);
    }

    public void GetError()
    {
        curErrorNumber = 0;
        totalErrorNumber = 5;
        errorText.text = "Error: " + curErrorNumber + " / " + totalErrorNumber;
    }

    public void StartLevel(string answer, string levelQuestion, int levelIndex)
    {
        this.answer = answer;
        questionField.transform.gameObject.GetComponent<TextMeshProUGUI>().text = levelQuestion;
        this.activeLevelIndex = levelIndex;
        CreateAnswerField();
        currentLevelObject.transform.gameObject.GetComponent<TextMeshProUGUI>().text = "Level " + levelIndex.ToString();
        coinText.text = CoinNumber.ToString();
        GetError();
    }
}
