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
    public ProjectileSpell spell;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = 6;
        //colour = GetComponent<SpriteRenderer>().color;
    }

    private void Start()
    {
        SetValues();
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
        element = spell.element;

        if (parent != null && parent.tag != "Enemy")
        {
            PlayerStats stats = parent.GetComponent<PlayerStats>();
            damage = (stats.damage.GetValue() + spell.damage) * (stats.damagePercentage.GetValue()/100 + 1);
        }
        else
        {
            CharacterStats stats = parent.GetComponent<CharacterStats>();
            damage = stats.damage.GetValue();
        }

        speed = spell.speed;
        range = spell.range;

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
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collision2D>().collider);
        }
        Destroy(gameObject);
    }
}
