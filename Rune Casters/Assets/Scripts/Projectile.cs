using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject parent;
    private Rigidbody2D rb;
    private Vector2 startPos;
    public int damage;
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
        if (parent != null && parent.tag != "Enemy")
        {
            PlayerStats stats = parent.GetComponent<PlayerStats>();
            damage = stats.damage.GetValue();
        }
        else
        {
            CharacterStats stats = parent.GetComponent<CharacterStats>();
            damage = stats.damage.GetValue();
        }
        speed = 10;
        range = 10;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
