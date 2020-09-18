using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    TextMeshProUGUI dialogText;
    TMP_InputField inputField;
    string stringInput;

    public GameObject star;
    void Start()
    {
        dialogText = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        inputField = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();
    }

    void Update()
    {
        dialogText.text = "Testing";
    }

    public void DisableDialog()
    {
        gameObject.SetActive(false);
    }

    public void Submit()
    {
        stringInput = inputField.text;
        Debug.Log(stringInput);
        if (stringInput == "star")
        {
            star.SetActive(true);
        }
    }
}
