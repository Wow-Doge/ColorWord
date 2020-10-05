using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessManager : SingletonComponent<GuessManager>
{
    [System.Serializable]
    public class CategoryInfo
    {
        public string name;
        public string displayName;
        public List<LevelInfo> levelInfo;
    }
    [SerializeField]
    private List<CategoryInfo> categoryInfos;

    [System.Serializable]
    public class LevelInfo
    {
        public string answer;
        public string question;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GetCategoryInfos(GameObject categoryInformation)
    {
        for (int i = 0; i < categoryInfos.Count; i++)
        {
            Instantiate(categoryInformation);
        }
    }
}
