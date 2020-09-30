using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGameplay : MonoBehaviour
{
    public WordManager wordManager;

    public float wordDelay = 1.5f;
    public float nextWordtime = 0f;

    public enum State
    {
        Idle,
        Start,
        End,
    }

    private State state;
    void Start()
    {
        state = State.Idle;  
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Start:
                if (Time.time >= nextWordtime)
                {
                    wordManager.AddWord();
                    nextWordtime = Time.time + wordDelay;
                    wordDelay *= .99f;
                }
                break;
            case State.End:
                break;
        }

        if (wordManager.wordCount >= 10)
        {
            state = State.End;
            return;
        }
    }

    public void StartGame()
    {
        state = State.Start;
    }
}
