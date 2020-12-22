using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseScreen : MonoBehaviour
{
    public void OpenUIScreen()
    {
        gameObject.SetActive(true);
        UIManager.Instance.PopupShow(gameObject);
    }

    public void CloseUIScreen()
    {
        UIManager.Instance.PopupHide(gameObject);
        StartCoroutine(CloseUI());
    }

    IEnumerator CloseUI()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
