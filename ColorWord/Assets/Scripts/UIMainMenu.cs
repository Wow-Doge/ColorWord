using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public void HideMainMenu()
    {
        gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        gameObject.SetActive(true);
    }
}
