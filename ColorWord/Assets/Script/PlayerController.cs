using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    float horizontalMove;
    float speed = 5f;
    public TextMeshProUGUI text;
    public GameObject inputField;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {  
        
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(horizontalMove * speed, rb2d.position.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && Input.GetKeyDown(KeyCode.T))
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<NPCController>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<NPCController>().enabled = false;
    }
}
