using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public GameObject startMenu;
    public void PlayButton()
    {
        startMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
