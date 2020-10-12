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
    public string answer;

    private const char placeholder = '_';

    private string userInput;

    Transform answerField;

    void Start()
    {
        answerField = guessGameplay.transform.GetChild(0).gameObject.transform;
    }

    void Update()
    {
        
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
            Instantiate(letterPrefab, new Vector3(1f * i - 1f, answerField.position.y, answerField.position.z), Quaternion.identity, answerField);
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
                TextMeshProUGUI text = answerField.GetChild(i).gameObject.transform.GetComponent<TextMeshProUGUI>();
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
        yield return new WaitForSeconds(1f);
        uICompleteScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        uICompleteScreen.SetActive(false);
        ReturnToUILevel();
    }

    public void ReturnToUILevel()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        uILevel.ShowLevel();
        guessGameplay.SetActive(false);
    }
}
