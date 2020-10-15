using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using UnityEngine;

public class LevelListItem : MonoBehaviour
{
    public string levelName;
    public int levelIndex;
    public string levelQuestion;
    public string levelAnswer;

    TextMeshProUGUI levelNameText;

    public GameObject completedLevelImage;
    public GameObject lockedLevelImage;

    public enum Type
    {
        Normal,
        Completed,
        Locked
    }

    public Type type;
    void Start()
    {
        levelNameText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        levelNameText.text = levelName;
    }

    void Update()
    {
        
    }

    public void Setup(Type type)
    {
        this.type = type;

        completedLevelImage.gameObject.SetActive(type == Type.Completed);
        lockedLevelImage.gameObject.SetActive(type == Type.Locked);
    }
    public void GetLevelInfo()
    {
        if (type != Type.Locked)
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject guessGameplayObject = canvas.gameObject.transform.GetChild(4).gameObject;
            guessGameplayObject.SetActive(true);
            GuessGameplay.Instance.answer = levelAnswer.ToUpper();
            GuessGameplay.Instance.CreateAnswerField();
            GuessGameplay.Instance.questionField.gameObject.transform.GetComponent<TextMeshProUGUI>().text = levelQuestion;
            GuessGameplay.Instance.activeLevelIndex = levelIndex;
        }

    }

    public void HideUILevel()
    {
        if (type != Type.Locked)
        {
            GameObject uILevel = GameObject.Find("UILevel");
            RectTransform rectTransform = uILevel.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(-900, 0);
            rectTransform.offsetMax = new Vector2(-900, 0);
        }
    }
}
