using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoggleMode : MonoBehaviour
{
    public void ShowMode()
    {
        gameObject.SetActive(true);
    }

    public void HideMode()
    {
        gameObject.SetActive(false);
    }
}
