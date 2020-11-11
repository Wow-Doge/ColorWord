using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CongratulationText : MonoBehaviour
{
    private readonly string[] displayTexts = new string[] { "Good", "Amazing", "Wonderful", "Great" };
    TextMeshProUGUI textTMP;

    private void OnEnable()
    {
        textTMP = gameObject.GetComponent<TextMeshProUGUI>();
        textTMP.text = displayTexts[Random.Range(0, displayTexts.Length)];
    }
}
