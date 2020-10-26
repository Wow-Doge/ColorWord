﻿using System.Collections;
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
