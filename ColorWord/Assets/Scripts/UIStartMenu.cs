using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void BackButton()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
