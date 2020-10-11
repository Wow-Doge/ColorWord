using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Transform levelListContainer;
    public GameObject levelListPrefab;

    public string activeCategoryName;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void DisplayUILevel()
    {
        gameObject.SetActive(true);
    }

    public void DisplayLevel()
    {
        for (int i = 0; i < GuessManager.Instance.CategoryInfos.Count; i++)
        {
            CategoryInfo categoryInfo = GuessManager.Instance.CategoryInfos[i];
            if (activeCategoryName == categoryInfo.name)
            {
                for (int j = 0; j < categoryInfo.levelInfos.Count; j++)
                {
                    Instantiate(levelListPrefab, levelListContainer);
                }
            }
        }
    }

    public void ShowLevel()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
        Debug.Log("Ok");
    }
}
