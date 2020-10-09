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

    public void Display()
    {
        gameObject.SetActive(true);
    }

    public void ShowCategory()
    {
        for (int i = 0; i < GuessManager.Instance.CategoryInfos.Count; i++)
        {
            CategoryInfo categoryInfo = GuessManager.Instance.CategoryInfos[i];
            CategoryListItem categoryListItem = gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).transform.GetComponent<CategoryListItem>();
            //CategoryListItem categoryListItem = categoryListPrefab.GetComponent<CategoryListItem>();
            categoryListItem.Setup(categoryInfo);
        }
    }

    public void DisableUICategory()
    {
        gameObject.SetActive(false);
    }

    public void SetupLevel()
    {

    }

}
