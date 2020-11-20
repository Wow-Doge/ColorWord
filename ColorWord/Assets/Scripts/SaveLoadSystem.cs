using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static void Save()
    {
        PlayerData playerData = new PlayerData();
        Debug.Log(JsonUtility.ToJson(playerData));
        string path = Application.dataPath + "/text.txt";
        File.WriteAllText(path, JsonUtility.ToJson(playerData));
    }

    public static PlayerData Load()
    {
        if (File.Exists(Application.dataPath + "/text.txt"))
        {
            string loadPath = File.ReadAllText(Application.dataPath + "/text.txt");
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(loadPath);
            return playerData;
        }
        else
        {
            File.Create(Application.dataPath + "/text.txt");
            return null;
        }
    }
}
