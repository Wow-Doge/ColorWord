using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Transform levelListContainer;
    public GameObject levelListPrefab;

    public string activeCategoryName;

    GameObject levelListObject;

    public GameObject uILevelTopBar;

    public int numberOfAvailableLevel;

    public void DisplayUILevel()
    {
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        numberOfAvailableLevel = 1;
    }
    public void DisplayLevel()
    {
        for (int i = 0; i < GuessManager.Instance.CategoryInfos.Count; i++)
        {
            CategoryInfo categoryInfo = GuessManager.Instance.CategoryInfos[i];
            if (activeCategoryName == categoryInfo.name)
            {
                for (int j = 0; j < categoryInfo.levelInfos.Count; j++)
                {
                    levelListObject = Instantiate(levelListPrefab, levelListContainer);
                    LevelListItem levelListItem = levelListObject.GetComponent<LevelListItem>();
                    levelListItem.levelQuestion = categoryInfo.levelInfos[j].question;
                    levelListItem.levelAnswer = categoryInfo.levelInfos[j].answer;
                    levelListItem.levelName = categoryInfo.levelInfos[j].name;
                    levelListItem.levelIndex = j + 1;
                    if (j + 1 <= numberOfAvailableLevel)
                    {
                        if (GuessGameplay.Instance.IsLevelCompleted(j + 1))
                        {
                            levelListItem.type = LevelListItem.Type.Completed;
                        }
                        else if (!GuessGameplay.Instance.IsLevelCompleted(j + 1))
                        {
                            levelListItem.type = LevelListItem.Type.Normal;
                        }
                    }
                    else if (j + 1 > numberOfAvailableLevel)
                    {
                        levelListItem.type = LevelListItem.Type.Locked;
                    }
                    levelListItem.Setup(levelListItem.type);
                }
            }
        }
    }
    
    public void ShowLevel()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }

    public void HideLevel()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(-900, 0);
        rectTransform.offsetMax = new Vector2(-900, 0);
        foreach (Transform childObject in levelListContainer)
        {
            Destroy(childObject.gameObject);
        }
    }

    public int CountAvailableLevel()
    {
        if (GuessGameplay.Instance.CheckWinCondition())
        {
            numberOfAvailableLevel += 1;
        }
        return numberOfAvailableLevel;
    }
}
