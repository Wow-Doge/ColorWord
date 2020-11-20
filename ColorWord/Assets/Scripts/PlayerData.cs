using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public List<DataList> dataList = GuessGameplay.Instance.dataList;
    public int coins = GuessGameplay.Instance.CoinNumber;
    public int rewardsIndex;
}

[System.Serializable]
public class DataList
{
    public string categoryName;
    public int completedLevel;

    public DataList(CategoryListItem categoryListItem)
    {
        categoryName = categoryListItem.categoryName;
        completedLevel = categoryListItem.NumOfActiveLevel;
    }
}
