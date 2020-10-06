using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GuessManager : SingletonComponent<GuessManager>
{
    [System.Serializable]
    public class CategoryInfo
    {
        public string name;
        public string displayName;
        public List<LevelInfo> levelInfos;
    }

    [SerializeField]
    private List<CategoryInfo> categoryInfos;

    [System.Serializable]
    public class LevelInfo
    {
        public string name;
        public int levelID;
        public string question;
        public string answer;
    }

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
}
