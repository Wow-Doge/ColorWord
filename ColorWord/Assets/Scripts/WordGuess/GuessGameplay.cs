using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System;

public class GuessGameplay : MonoBehaviour
{
    public GameObject guessGameplay;
    public GameObject letterPrefab;

    [SerializeField]
    private string answer;

    private const char placeholder = '_';

    private string userInput;

    Transform answerTextTransform;

    void Start()
    {
        answerTextTransform = guessGameplay.transform.GetChild(0).gameObject.transform;
        CreateAnswerField();
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
        for (int i = 0; i < answer.Length; i++)
        {
            sb.Append(placeholder);
            userInput = sb.ToString();
            Instantiate(letterPrefab, new Vector3(1f * i - 1f, answerTextTransform.position.y, answerTextTransform.position.z), Quaternion.identity, answerTextTransform);
            TextMeshProUGUI textLetter = answerTextTransform.GetComponentInChildren<TextMeshProUGUI>();
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
                Debug.Log("Congratulation");
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
                TextMeshProUGUI text = answerTextTransform.GetChild(i).gameObject.transform.GetComponent<TextMeshProUGUI>();
                text.text = letter.ToString();
            }
        }
        userInput = new string(userInputArray);
    }

    public bool CheckWinCondition()
    {
        return answer.Equals(userInput);
    }
}
