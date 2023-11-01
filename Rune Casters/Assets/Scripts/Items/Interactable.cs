using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1.0f;
    public bool canPickup = false;

    private void Awake()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();

        rb.gravityScale = 0;
        collider.radius = radius;
        collider.isTrigger = true;
    }

    public virtual void Interact()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Interact();
        }
    }
}
