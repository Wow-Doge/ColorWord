using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : SingletonComponent<UIManager>
{
    public void Show(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        //rectTransform.offsetMin = new Vector2(0, 0);
        //rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.25f);
    }

    public void Hide(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        //rectTransform.offsetMin = new Vector2(900, 0);
        //rectTransform.offsetMax = new Vector2(900, 0);
        rectTransform.DOAnchorPos(new Vector2(900, 0), 0.25f);
    }

    public void PopupShow(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.DOScale(new Vector3(1, 1, 1), 0.15f);
    }
    
    public void PopupHide(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.DOScale(new Vector3(0, 0, 0), 0.15f);
    }
}
