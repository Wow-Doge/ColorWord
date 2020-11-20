using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class GameEvent
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnPress)
    {
        button.onClick.AddListener(delegate ()
        {
            OnPress(param);
        });
    }
}
