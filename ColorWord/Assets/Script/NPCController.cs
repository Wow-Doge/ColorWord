using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
public class NPCController : MonoBehaviour, IInteractable
{
    GameObject canvas;
    GameObject dialog;
    GameObject dialogSystem;
    TextMeshProUGUI dialogText;
    Button submitButton;
    [SerializeField]
    private string npcDialog;

    GameObject resultDialog;
    TextMeshProUGUI resultDialogText;
    Button resultDialogButton;

    Button quitButton;

    [SerializeField]
    private string rightAnswer;
    [SerializeField]
    private string wrongAnswer;

    //public event EventHandler OnPlayerStartInteract;

    [SerializeField]
    private string keyWord;
    private string inputString;
    TMP_InputField inputField;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        dialog = canvas.transform.GetChild(0).gameObject;

        dialogSystem = dialog.transform.GetChild(0).gameObject;
        dialogText = dialogSystem.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        resultDialog = dialogSystem.transform.GetChild(1).gameObject;
        resultDialogText = resultDialog.GetComponent<TextMeshProUGUI>();
        resultDialogButton = resultDialog.transform.GetChild(0).gameObject.GetComponent<Button>();

        inputField = dialog.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();
        submitButton = dialog.transform.GetChild(2).gameObject.GetComponent<Button>();

        quitButton = dialog.transform.GetChild(3).gameObject.GetComponent<Button>();
        quitButton.onClick.AddListener(DisableDialog);

        //OnPlayerStartInteract += Highlight;
    }

    //private void Highlight(object sender, EventArgs e)
    //{
    //    Debug.Log("Highlight");
    //}

    public void Interact()
    {
        dialog.SetActive(true);
        //OnPlayerStartInteract?.Invoke(this, EventArgs.Empty);
        dialogText.enabled = true;
        dialogText.text = npcDialog;
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(CheckWord);
    }

    private void CheckWord()
    {
        inputString = inputField.text;
        if (string.Equals(keyWord, inputString))
        {
            ResultDialog(rightAnswer);
            Debug.Log("Match");
        }
        else
        {
            ResultDialog(wrongAnswer);
            Debug.Log("Mismatch");
        }
    }

    void ResultDialog(string resultText)
    {
        resultDialog.SetActive(true);
        resultDialogText.text = resultText;
        dialogText.enabled = false;
        resultDialogButton.onClick.RemoveAllListeners();
        resultDialogButton.onClick.AddListener(DisableDialog);
    }

    public void DisableDialog()
    {
        resultDialog.SetActive(false);
        dialog.SetActive(false);
    }
}
