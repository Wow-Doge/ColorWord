using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Transform levelListContainer;
    public GameObject levelListPrefab;
    void Start()
    {
        DisplayLevel();
    }
    void Update()
    {
        
    }

    public void Display()
    {
        gameObject.SetActive(true);
    }

    public void DisplayLevel()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(levelListPrefab, levelListContainer);
        }
    }
}
