using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
    public class CategoryInfo
    {
        public string name;
        public string displayName;
        public List<LevelInfo> levelInfos;
    }

    [System.Serializable]
    public class LevelInfo
    {
        public string name;
        [TextArea]
        public string question;
        public string answer;
        public Sprite sprite;
        [TextArea]
        public string description;
    }

public class GuessManager : SingletonComponent<GuessManager>
{
    [SerializeField]
    private List<CategoryInfo> categoryInfos;

    public void GetCategoryInfos(GameObject categoryPrefab, Transform categoryListContainer)
    {
        for (int i = 0; i < categoryInfos.Count; i++)
        {
            Instantiate(categoryPrefab, categoryListContainer);
        }
    }

    public List<CategoryInfo> CategoryInfos
    {
        get
        {
            return categoryInfos;
        }
    }

    public CategoryInfo GetCategoryInfo(string categoryName)
    {
        for (int i = 0; i < CategoryInfos.Count; i++)
        {
            if (categoryName == categoryInfos[i].name)
            {
                return CategoryInfos[i];
            }
        }
        return null;
    }
}
