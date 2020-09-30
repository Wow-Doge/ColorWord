using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessGameplay : MonoBehaviour
{
    public enum State
    {
        Start,
        End,
    }

    private State state;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGuess()
    {
        state = State.Start;
        Debug.Log("Start the game");
    }
}
