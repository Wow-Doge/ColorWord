using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Transform levelListContainer;
    public GameObject levelListPrefab;

    public string activeCategoryName;

    public GameObject uILevelTopBar;

    private ObjectPool levelItemObjectPool;

    public int numbersOfActiveLevel;
    public void DisplayUILevel()
    {
        gameObject.SetActive(true);
    }

    public void Start()
    {
        levelItemObjectPool = new ObjectPool(levelListPrefab, 10, levelListContainer);
    }
    //public void DisplayLevel()
    //{
    //    for (int i = 0; i < GuessManager.Instance.CategoryInfos.Count; i++)
    //    {
    //        CategoryInfo categoryInfo = GuessManager.Instance.CategoryInfos[i];
    //        if (activeCategoryName == categoryInfo.name)
    //        {
    //            for (int j = 0; j < categoryInfo.levelInfos.Count; j++)
    //            {
    //                levelListObject = Instantiate(levelListPrefab, levelListContainer);
    //                LevelListItem levelListItem = levelListObject.GetComponent<LevelListItem>();

    //                LevelListItem.Type type;
    //                if (GuessGameplay.Instance.IsLevelCompleted(activeCategoryName, j + 1))
    //                {
    //                    type = LevelListItem.Type.Completed;
    //                }
    //                else
    //                {
    //                    type = LevelListItem.Type.Normal;
    //                }
    //                levelListItem.Setup(categoryInfo.levelInfos[j].name, j + 1, categoryInfo.levelInfos[j].question, categoryInfo.levelInfos[j].answer, type);
    //                levelListObject.gameObject.SetActive(true);
    //            }
    //        }
    //    }
    //}

    public void SetupCategoryListItem(string categoryName, int activeLevelNumber)
    {
        this.activeCategoryName = categoryName;
        this.numbersOfActiveLevel = activeLevelNumber;
    }

    public void DisplayLevel()
    {
        ////Get information of current category
        CategoryInfo categoryInfo = GuessManager.Instance.GetCategoryInfo(activeCategoryName);

        levelItemObjectPool.ReturnAllObjectsToPool();

        for (int i = 0; i < categoryInfo.levelInfos.Count; i++)
        {
            LevelListItem.Type type;    
            if (i < numbersOfActiveLevel)
            {
                type = LevelListItem.Type.Completed;
            }
            else if (i == numbersOfActiveLevel)
            {
                type = LevelListItem.Type.Normal;
            }
            else
            {
                type = LevelListItem.Type.Locked;
            }
            //LevelListItem.Type type = completed ? LevelListItem.Type.Completed : LevelListItem.Type.Locked;
            //if (completed && !GuessGameplay.Instance.IsLevelCompleted(categoryInfo.name, i + 1))
            //{
            //    completed = false;
            //    type = LevelListItem.Type.Normal;
            //}

            //LevelListItem.Type type;
            //type = LevelListItem.Type.Normal;

            LevelListItem levelListItem = levelListContainer.GetChild(i).transform.gameObject.GetComponent<LevelListItem>();

            levelListItem.Setup(categoryInfo, i + 1, type, categoryInfo.levelInfos[i].answer, categoryInfo.levelInfos[i].question);
            levelListItem.gameObject.SetActive(true);
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
            childObject.gameObject.SetActive(false);
        }
    }
}
