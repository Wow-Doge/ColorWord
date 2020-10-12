using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Transform levelListContainer;
    public GameObject levelListPrefab;

    public string activeCategoryName;

    GameObject levelListObject;

    public GameObject uILevelTopBar;
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
                    levelListObject = Instantiate(levelListPrefab, levelListContainer);
                    LevelListItem levelListItem = levelListObject.GetComponent<LevelListItem>();
                    levelListItem.levelQuestion = categoryInfo.levelInfos[j].question;
                    levelListItem.levelAnswer = categoryInfo.levelInfos[j].answer;
                    levelListItem.levelName = categoryInfo.levelInfos[j].name;
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

    public void HideLevel()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(-900, 0);
        rectTransform.offsetMax = new Vector2(-900, 0);
        foreach (Transform childObject in levelListContainer)
        {
            Destroy(childObject.gameObject);
        }
    }
}
