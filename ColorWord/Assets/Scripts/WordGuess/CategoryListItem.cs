using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryListItem : MonoBehaviour
{
    public GameObject uiCategory;
    void Start()
    {
        
    }

    public void Setup(CategoryInfo categoryInfo)
    {
        TextMeshProUGUI categoryText;
        string categoryName;
        categoryText = gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>();
        categoryName = categoryInfo.name;
        categoryText.text = categoryName;
    }
}
