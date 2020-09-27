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
    GameObject inputSystem;
    TMP_InputField inputField;
    Button submitButton;
    Button skipButton;
    GameObject notification;
    Button skipNotification;

    PlayerController playerController;

    [SerializeField]
    private string keyWord;
    [SerializeField]
    private string npcDialog;

    private string inputString;

    bool isAnswered;

    StarCount starCount;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        isAnswered = false;
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        dialog = canvas.transform.GetChild(0).gameObject;

        dialogSystem = dialog.transform.GetChild(0).gameObject;
        dialogText = dialogSystem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        inputSystem = dialog.transform.GetChild(1).gameObject;
        inputField = inputSystem.transform.GetChild(0).gameObject.GetComponent<TMP_InputField>();
        submitButton = inputSystem.transform.GetChild(1).gameObject.GetComponent<Button>();
        skipButton = inputSystem.transform.GetChild(2).gameObject.GetComponent<Button>();

        notification = dialog.transform.GetChild(2).gameObject;
        skipNotification = notification.transform.GetChild(1).gameObject.GetComponent<Button>();

        starCount = canvas.transform.GetChild(1).gameObject.GetComponent<StarCount>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        

        skipButton.onClick.AddListener(SkipDialog);
    }

    public void Interact()
    {
        playerController.canMove = false;
        if (isAnswered == true)
        {
            dialog.SetActive(true);
            dialogText.text = "thank you";
            StopAllCoroutines();
            notification.SetActive(false);
            StartCoroutine(AutoDisableDialog(dialog));
        }
        else
        {
            dialog.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(TypeDialog());
            submitButton.onClick.RemoveAllListeners();
            submitButton.onClick.AddListener(CheckWord);
        }
    }

    IEnumerator TypeDialog ()
    {
        dialogText.text = "";
        foreach (char letter in npcDialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.02f); 
        }
    }

    private void CheckWord()
    {
        inputString = inputField.text;
        if (string.Equals(keyWord, inputString))
        {
            RightAnswer();
            Debug.Log("Match");
        }
        else
        {
            WrongAnswer();
            Debug.Log("Mismatch");
        }
    }


    private void RightAnswer()
    {
        Destroy(spriteRenderer.material);
        starCount.numOfStar += 1;
        isAnswered = true;
        notification.SetActive(true);
        TextMeshProUGUI text = notification.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "thanks";
        skipNotification.onClick.RemoveAllListeners();
        skipNotification.onClick.AddListener(SkipNotification);
        skipNotification.onClick.AddListener(SkipDialog);
    }

    private void WrongAnswer()
    {
        notification.SetActive(true);
        TextMeshProUGUI text = notification.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "Wrong answer";
        skipNotification.onClick.RemoveAllListeners();
        skipNotification.onClick.AddListener(SkipNotification);
    }
    private void SkipDialog()
    {
        playerController.canMove = true;
        dialog.SetActive(false);
    }
    private void SkipNotification()
    {
        notification.SetActive(false);
    }

    IEnumerator AutoDisableDialog(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.25f);
        gameObject.SetActive(false);
        playerController.canMove = true;
    }
}
