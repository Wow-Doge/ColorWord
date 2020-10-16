using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using UnityEngine;

public class LevelListItem : MonoBehaviour
{
    //[SerializeField]
    //private string levelName;
    //[SerializeField]
    //private int levelIndex;
    [SerializeField]
    private string levelQuestion;
    [SerializeField]
    private string levelAnswer;

    TextMeshProUGUI levelNameText;

    public GameObject completedLevelImage;
    public GameObject lockedLevelImage;

    private string categoryName;
    [SerializeField]
    private int levelIndex;
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
        levelNameText.text = levelIndex.ToString();
    }

    //public void Setup(string levelName, int levelIndex, string levelQuestion, string levelAnswer,Type type)
    //{
    //    this.type = type;
    //    this.levelName = levelName;
    //    this.levelIndex = levelIndex;
    //    this.levelQuestion = levelQuestion;
    //    this.levelAnswer = levelAnswer;

    //    completedLevelImage.gameObject.SetActive(type == Type.Completed);
    //    lockedLevelImage.gameObject.SetActive(type == Type.Locked);
    //}

    public void Setup(CategoryInfo categoryInfo, int levelIndex, Type type, string levelAnswer, string levelQuestion)
    {
        this.categoryName = categoryInfo.name;
        this.levelIndex = levelIndex;
        this.type = type;
        this.levelAnswer = levelAnswer;
        this.levelQuestion = levelQuestion;

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
            //GuessGameplay.Instance.answer = levelAnswer.ToUpper();
            //GuessGameplay.Instance.CreateAnswerField();
            //GuessGameplay.Instance.questionField.gameObject.transform.GetComponent<TextMeshProUGUI>().text = levelQuestion;
            //GuessGameplay.Instance.activeLevelIndex = levelIndex;
            GuessGameplay.Instance.StartLevel(levelAnswer, levelQuestion, levelIndex);
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
