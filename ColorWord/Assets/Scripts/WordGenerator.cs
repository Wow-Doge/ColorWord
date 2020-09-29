using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] wordList = { "balance", "hate", "love", "blind", "chemical", "void", "black", "cat", "animal", "consider",
                                         "power", "background", "music", "game", "bless", "retire", "state", "better", "marry", "trick",
                                         "smile", "laugh", "eleven", "receive", "crap", "zebra", "trample", "absolute", "zero", "fine", 
                                         "temp", "propose", "generate", "know", "electric", "keyboard", "mouse", "computer", "table", "chair"};
    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        string randomWord = wordList[randomIndex];
        return randomWord;
    }
}
