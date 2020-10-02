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

    private string placeholder = "_";

    private string userInput;

    Transform letterTransform;
    void Start()
    {
        letterTransform = guessGameplay.transform.GetChild(0).gameObject.transform;
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
        for (int i = 0; i < answer.Length; i++)
        {
            Instantiate(letterPrefab, new Vector3(1f * i, letterTransform.position.y, letterTransform.position.z), Quaternion.identity, letterTransform);
            TextMeshProUGUI textLetter = letterTransform.GetComponentInChildren<TextMeshProUGUI>();
            textLetter.text = placeholder.ToString();
        }
    }

    public void SubmitGuessLetter(Button button)
    {
        char letter = button.GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[0];
        if (answer.Contains(letter))
        {
            int index = answer.IndexOf(letter);
            Debug.Log(index);
            TextMeshProUGUI textLetterPosition = letterTransform.GetChild(index).gameObject.GetComponent<TextMeshProUGUI>();
            textLetterPosition.text = letter.ToString();
        }
    }
}
