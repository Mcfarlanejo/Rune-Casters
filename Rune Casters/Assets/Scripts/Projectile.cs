using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPos;
    public float damage;
    public Element element = Element.Water;
    public Color colour;
    public float speed;
    public float range;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //colour = GetComponent<SpriteRenderer>().color;
    }

    private void SetColour()
    {
        switch (element)
        {
            case Element.Basic:
                colour = Color.black;
                break;
            case Element.Fire:
                colour = Color.red;
                break;
            case Element.Earth:
                colour = Color.magenta;
                break;
            case Element.Water:
                colour = Color.blue;
                break;
            case Element.Wind:
                colour = Color.green;
                break;
            default:
                break;
        }
        GetComponent<SpriteRenderer>().color = colour;
    }

    public void SetValues()
    {
        speed = 10;
        range = 5;
        startPos = transform.position;
        SetColour();
        rb.velocity = transform.position * speed;
    }

    private void Update()
    {
        float traveled = Vector2.Distance(transform.position, startPos);
        if (traveled > range)
        {
            Destroy(gameObject);
        }
    }
}
