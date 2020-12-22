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

[System.Serializable]
public class ChallengeInfo
{
    public string title;
    public int challengeIndex;
    public string challengeDescription;
    public int goal;
}
public class GuessManager : SingletonComponent<GuessManager>
{
    [SerializeField]
    private List<CategoryInfo> categoryInfos;

    [SerializeField]
    private List<ChallengeInfo> challengeInfos;

    //public void GetCategoryInfos(GameObject categoryPrefab, Transform categoryListContainer)
    //{
    //    for (int i = 0; i < categoryInfos.Count; i++)
    //    {
    //        Instantiate(categoryPrefab, categoryListContainer);
    //    }
    //}

    public List<CategoryInfo> CategoryInfos
    {
        get
        {
            return categoryInfos;
        }
    }

    public List<ChallengeInfo> ChallengeInfos
    {
        get
        {
            return challengeInfos;
        }
    }

    public ChallengeInfo GetChallengeInfo(string name)
    {
        for (int i = 0; i < ChallengeInfos.Count; i++)
        {
            if (name == challengeInfos[i].title)
            {
                return ChallengeInfos[i];
            }
        }
        return null;
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
