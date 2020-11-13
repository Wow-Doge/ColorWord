using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonComponent<UIManager>
{
    public void Show(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }

    public void Hide(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(900, 0);
        rectTransform.offsetMax = new Vector2(900, 0);
    }
}
