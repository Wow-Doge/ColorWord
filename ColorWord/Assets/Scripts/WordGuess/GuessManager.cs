using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
        public string question;
        public string answer;
    }
public class GuessManager : SingletonComponent<GuessManager>
{
    [SerializeField]
    private List<CategoryInfo> categoryInfos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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

    //public CategoryInfo GetCategoryInfo()
    //{
    //    for (int i = 0; i < CategoryInfos.Count; i++)
    //    {
    //        return CategoryInfos[i];
    //    }
    //    return null;
    //}
}
