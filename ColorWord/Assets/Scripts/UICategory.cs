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

    private ObjectPool categoryItemObjectPool;

    void Awake()
    {
        categoryItemObjectPool = new ObjectPool(categoryListPrefab, 5, categoryListContainer);
    }
    private void Update()
    {
        SetupCategory();
    }

    public void SetupCategory()
    {
        categoryItemObjectPool.ReturnAllObjectsToPool();

        for (int i = 0; i < GuessManager.Instance.CategoryInfos.Count; i++)
        {
            CategoryInfo categoryInfo = GuessManager.Instance.CategoryInfos[i];
            CategoryListItem categoryListItem = gameObject.transform.GetChild(1).GetChild(i).transform.GetComponent<CategoryListItem>();
            categoryListItem.Setup(categoryInfo);
            categoryListItem.gameObject.SetActive(true);
        }
    }
    public void DisplayUICategory()
    {
        gameObject.SetActive(true);
    }

    public void HideCategory()
    {
        UIManager.Instance.Hide(gameObject);
    }

    public void ShowCategory()
    {
        UIManager.Instance.Show(gameObject);
    }
}
