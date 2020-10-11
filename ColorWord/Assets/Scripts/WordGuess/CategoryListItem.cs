using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryListItem : MonoBehaviour
{
    TextMeshProUGUI categoryText;
    public string categoryName;
    void Start()
    {
        
    }

    public void Setup(CategoryInfo categoryInfo)
    {
        
        float numOfLevels = categoryInfo.levelInfos.Count;

        categoryText = gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>();
        categoryName = categoryInfo.name;
        categoryText.text = categoryName;
        GetNumberOfLevels(numOfLevels);
    }

    public float GetNumberOfLevels(float number)
    {
        return number;
    }

    public void HideCategoryList()
    {
        UICategory uICategory = gameObject.GetComponentInParent<UICategory>();
        if (uICategory != null)
        {
            uICategory.HideCategory();
        }
    }

    public void GetCategoryInfo()
    {
        UILevel uILevel = GameObject.Find("UILevel").GetComponent<UILevel>();
        uILevel.ShowLevel();
        uILevel.activeCategoryName = categoryName;
        uILevel.DisplayLevel();
    }
}
