﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICategory : MonoBehaviour
{
    public Transform categoryListContainer;
    public GameObject categoryListPrefab;
    void Start()
    {
        GuessManager.Instance.GetCategoryInfos(categoryListPrefab, categoryListContainer);
    }

    void Update()
    {
        
    }

    public void Display()
    {
        gameObject.SetActive(true);
    }
}
