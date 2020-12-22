using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : SingletonComponent<UIManager>
{
    public void Show(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.25f);
    }

    public void Hide(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
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

    public void SlideDown(GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.25f);
    }

    public void SlideUp (GameObject data)
    {
        RectTransform rectTransform = data.GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(new Vector2(0, 1600), 0.25f);
    }

    //public void FadeIn(Image image)
    //{
    //    var tempColor = image.color;
    //    tempColor.a = 1f;
    //    image.color = tempColor;
    //}


    //public void FadeIn(Image image)
    //{
    //    for (float i = 1; i >= 0; i -= Time.deltaTime)
    //    {
    //        image.color = new Color(1, 1, 1, i);
    //    }
    //}

    public IEnumerator FadeIn(Image image)
    {
        image.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(image.color.r, image.color.g, image.color.b, i);
            yield return null;
        }
        //yield return new WaitForSeconds(1f);
        //image.color = new Color(0, 0, 0, 0);
        //image.gameObject.SetActive(false);
    }

    public void FadeOut(Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        image.gameObject.SetActive(false);
    }

    public IEnumerator ScaleUp(RectTransform rect)
    {
        rect.transform.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            Vector3 localTemp = rect.transform.localScale;
            localTemp.x = i;
            localTemp.y = i;
            localTemp.z = i;
            rect.transform.localScale = localTemp;
            //rect.localScale = new Vector3(localTemp.x, localTemp.y, localTemp.z);
        }
        yield return new WaitForSeconds(2f);
        rect.transform.gameObject.SetActive(false);
    }
}
