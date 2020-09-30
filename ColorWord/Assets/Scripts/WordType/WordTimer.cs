using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public WordManager wordManager;

    public float wordDelay = 1.5f;
    public float nextWordtime = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time >= nextWordtime)
        {
            wordManager.AddWord();
            nextWordtime = Time.time + wordDelay;
            wordDelay *= .99f;
        }
    }
}
