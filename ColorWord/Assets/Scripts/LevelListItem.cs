﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using UnityEngine;

public class LevelListItem : MonoBehaviour
{
    public string levelName;
    public string levelQuestion;
    public string levelAnswer;

    TextMeshProUGUI levelNameText;
    void Start()
    {
        levelNameText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        levelNameText.text = levelName;
    }

    void Update()
    {
          
    }

    public void GetLevelInfo()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject guessGameplayObject = canvas.gameObject.transform.GetChild(4).gameObject;
        guessGameplayObject.SetActive(true);
        GuessGameplay.Instance.answer = levelAnswer.ToUpper();
        GuessGameplay.Instance.CreateAnswerField();
        GuessGameplay.Instance.questionField.gameObject.transform.GetComponent<TextMeshProUGUI>().text = levelQuestion;
    }

    public void HideUILevel()
    {
        GameObject uILevel = GameObject.Find("UILevel");
        RectTransform rectTransform = uILevel.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(-900, 0);
        rectTransform.offsetMax = new Vector2(-900, 0);
        Debug.Log("Hide");
    }
}
