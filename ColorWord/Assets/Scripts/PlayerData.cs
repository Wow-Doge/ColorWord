using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]

public class PlayerData
{
    public string categoryName;
    public int completedLevel;

    public PlayerData(CategoryListItem categoryListItem)
    {
        categoryName = categoryListItem.categoryName;
        completedLevel = categoryListItem.NumOfActiveLevel;
    }
}
