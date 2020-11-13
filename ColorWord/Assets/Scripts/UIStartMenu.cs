using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartMenu : MonoBehaviour
{
    public void ShowStartMenu()
    {
        gameObject.SetActive(true);
    }

    public void HideStartMenu()
    {
        gameObject.SetActive(false);
    }
}
