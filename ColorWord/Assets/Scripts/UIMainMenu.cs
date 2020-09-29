using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public GameObject startMenu;
    public void StartButton()
    {
        startMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
