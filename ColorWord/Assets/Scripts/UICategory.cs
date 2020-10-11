using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICategory : MonoBehaviour
{
    public Transform categoryListContainer;
    public GameObject categoryListPrefab;

    void Start()
    {
        GuessManager.Instance.GetCategoryInfos(categoryListPrefab, categoryListContainer);
        ShowCategory();
    }

    void Update()
    {
        
    }

    public void DisplayUICategory()
    {
        gameObject.SetActive(true);
    }

    public void ShowCategory()
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
}
