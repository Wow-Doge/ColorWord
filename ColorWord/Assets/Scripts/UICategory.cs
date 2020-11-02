using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICategory : MonoBehaviour
{
    public Transform categoryListContainer;
    public GameObject categoryListPrefab;

    public GameObject uICategoryTopBar;
    public GameObject startMenu;

    void Start()
    {
        GuessManager.Instance.GetCategoryInfos(categoryListPrefab, categoryListContainer);
        DisplayCategory();
    }

    public void DisplayUICategory()
    {
        gameObject.SetActive(true);
    }

    public void DisplayCategory()
    {
        for (int i = 0; i < GuessManager.Instance.CategoryInfos.Count; i++)
        {
            CategoryInfo categoryInfo = GuessManager.Instance.CategoryInfos[i];
            CategoryListItem categoryListItem = gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).transform.GetComponent<CategoryListItem>();
            categoryListItem.Setup(categoryInfo);
        }
    }

    public void HideCategory()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(900, 0);
        rectTransform.offsetMax = new Vector2(900, 0);
    }

    public void ShowCategory()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }

    public void DisableCategory()
    {
        gameObject.SetActive(false);
    }

    public void EnableStartMenu()
    {
        startMenu.SetActive(true);
    }
}
