using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelListItem : MonoBehaviour
{
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

    private Sprite sprite;
    private string description;

    [SerializeField]
    private GameObject timeModeGO;
    [SerializeField]
    private GameObject lockModeGO;
    public enum Type
    {
        Normal,
        Completed,
        Locked
    }

    private bool isTimeMode;
    private bool isLockMode;

    public Type type;
    void Start()
    {
        levelNameText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        levelNameText.text = levelIndex.ToString();
    }
    public void Setup(CategoryInfo categoryInfo, int levelIndex, Type type, string levelAnswer, string levelQuestion, Sprite sprite, string description)
    {
        this.categoryName = categoryInfo.name;
        this.levelIndex = levelIndex;
        this.type = type;
        this.levelAnswer = levelAnswer;
        this.levelQuestion = levelQuestion;
        this.sprite = sprite;
        this.description = description;

        completedLevelImage.gameObject.SetActive(type == Type.Completed);
        lockedLevelImage.gameObject.SetActive(type == Type.Locked);
    }

    public void GetLevelInfo()
    {
        if (type != Type.Locked)
        {
            GameObject guessMode = GameObject.Find("Canvas/GuessMode");
            GameObject guessGameplayObject = guessMode.gameObject.transform.GetChild(2).gameObject;
            guessGameplayObject.SetActive(true);
            GuessGameplay.Instance.StartLevel(levelAnswer, levelQuestion, levelIndex, type, sprite, description, isTimeMode, isLockMode);
        }
    }

    public void SetupMode(bool TimeMode, bool LockMode)
    {
        this.isTimeMode = TimeMode;
        this.isLockMode = LockMode;
        timeModeGO.SetActive(isTimeMode);
        lockModeGO.SetActive(isLockMode);
    }

    public void HideUILevel()
    {
        if (type != Type.Locked)
        {
            GameObject uILevel = GameObject.Find("UILevel");
            UIManager.Instance.Hide(uILevel);
        }
    }
}
