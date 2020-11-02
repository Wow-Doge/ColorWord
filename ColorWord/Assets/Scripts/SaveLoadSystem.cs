using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    //public static void SavePlayer()
    //{
    //    List<PlayerData> playerData = new List<PlayerData>();
    //    playerData = GuessGameplay.Instance.playerDataList;
    //    string json = "";
    //    foreach (var item in playerData)
    //    {
    //        json += " [ " + item.categoryName + " : " + item.completedLevel + " ] ";
    //    }
    //    Debug.Log(json);
    //    json = JsonUtility.ToJson(playerData);
    //    //Debug.Log(json);

    //    string path = Application.dataPath + "/text.txt";
    //    File.WriteAllText(path, json);
    //}
}
