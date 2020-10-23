using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseScreen : MonoBehaviour
{
    public void OpenUIScreen()
    {
        gameObject.SetActive(true);
    }

    public void CloseUIScreen()
    {
        gameObject.SetActive(false);
    }
}
