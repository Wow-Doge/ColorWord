using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class WordAPI : MonoBehaviour
{
    private string wordName;
    private string phonetics;
    private string definitions;
    private string example;

    public TextMeshProUGUI description;
    public IEnumerator GetAPI(string word)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.dictionaryapi.dev/api/v2/entries/en/" + word);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string text = www.downloadHandler.text;
            string newText = text.Substring(1, text.Length - 2);

            var wordInfo = JSON.Parse(newText);
            Debug.Log(wordInfo);

            wordName = wordInfo["word"];
            phonetics = wordInfo["phonetics"][0]["text"];
            definitions = wordInfo["meanings"][0]["definitions"][0]["definition"];
            example = wordInfo["meanings"][0]["definitions"][0]["example"];
            Debug.Log(wordName);
            Debug.Log(phonetics);
            Debug.Log(definitions);
            Debug.Log(example);
        }

        description.text = "Name: " + wordName.ToString() + "\n\n" + "Definition: " + definitions.ToString() + "\n\n" + "Example: " + example.ToString();
    }
}
