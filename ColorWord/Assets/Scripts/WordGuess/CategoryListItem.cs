using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryListItem : MonoBehaviour
{
    TextMeshProUGUI categoryText;
    TextMeshProUGUI categoryLevel;
    public string categoryName;

    public int NumOfActiveLevel { get; set; } = 0;
    public void Setup(CategoryInfo categoryInfo)
    {
        categoryText = gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>();
        categoryLevel = gameObject.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshProUGUI>();
        categoryName = categoryInfo.name;
        categoryText.text = categoryName.ToUpper();
        categoryLevel.text = "Levels: " + NumOfActiveLevel.ToString() + "/10";
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
        uILevel.SetupCategoryListItem(categoryName, NumOfActiveLevel);
        uILevel.SetupLevel();
        GuessGameplay.Instance.activeCategoryInfo = categoryName;
    }
}
