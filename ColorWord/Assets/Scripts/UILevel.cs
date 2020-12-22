using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Transform levelListContainer;
    public GameObject levelListPrefab;

    public string activeCategoryName;

    public GameObject uILevelTopBar;

    private ObjectPool levelItemObjectPool;

    public int numbersOfActiveLevel;
    public void DisplayUILevel()
    {
        gameObject.SetActive(true);
    }

    [SerializeField] private List<int> timeMode;
    [SerializeField] private List<int> lockMode;
    private void Awake()
    {
        levelItemObjectPool = new ObjectPool(levelListPrefab, 10, levelListContainer);
    }

    public void SetupCategoryListItem(string categoryName, int activeLevelNumber)
    {
        this.activeCategoryName = categoryName;
        this.numbersOfActiveLevel = activeLevelNumber;
    }

    public void SetupLevel()
    {
        ////Get information of current category
        CategoryInfo categoryInfo = GuessManager.Instance.GetCategoryInfo(activeCategoryName);

        levelItemObjectPool.ReturnAllObjectsToPool();

        for (int i = 0; i < categoryInfo.levelInfos.Count; i++)
        {
            LevelListItem.Type type;    
            if (i < numbersOfActiveLevel)
            {
                type = LevelListItem.Type.Completed;
            }
            else if (i == numbersOfActiveLevel)
            {
                type = LevelListItem.Type.Normal;
            }
            else
            {
                type = LevelListItem.Type.Locked;
            }
            LevelListItem levelListItem = levelListContainer.GetChild(i).transform.gameObject.GetComponent<LevelListItem>();

            levelListItem.Setup(categoryInfo, i + 1, type, categoryInfo.levelInfos[i].answer, categoryInfo.levelInfos[i].question, categoryInfo.levelInfos[i].sprite, categoryInfo.levelInfos[i].description);
            levelListItem.gameObject.SetActive(true);
        }

        for (int j = 0; j < categoryInfo.levelInfos.Count; j++)
        {
            LevelListItem levelListItem = levelListContainer.GetChild(j).transform.gameObject.GetComponent<LevelListItem>();
            levelListItem.SetupMode(timeMode.Contains(j+1), lockMode.Contains(j+1));
        }
    }
    
    public void ShowLevel()
    {
        UIManager.Instance.Show(gameObject);
    }

    public void HideLevel()
    {
        UIManager.Instance.Hide(gameObject);
    }
}
