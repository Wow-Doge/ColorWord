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

    public int numOfActiveLevel;

    private void Awake()
    {
        numOfActiveLevel = 0;
    }
    void Start()
    {
        
    }

    public void Setup(CategoryInfo categoryInfo)
    {
        categoryText = gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>();
        categoryName = categoryInfo.name;
        categoryText.text = categoryName;
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
        uILevel.SetupCategoryListItem(categoryName, numOfActiveLevel);
        //uILevel.activeCategoryName = categoryName;
        //uILevel.numbersOfActiveLevel = numOfActiveLevel;
        uILevel.DisplayLevel();
        GuessGameplay.Instance.activeCategoryInfo = categoryName;
    }
}
