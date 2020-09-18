using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    GameObject canvas;
    GameObject dialog;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        dialog = canvas.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        dialog.SetActive(true);
    }
}
