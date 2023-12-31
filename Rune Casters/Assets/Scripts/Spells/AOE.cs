using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public GameObject parent;
    private Rigidbody2D rb;
    private Vector3 startScale;
    public int damage;
    public Element element = Element.Water;
    public Color colour;
    public Vector3 speed;
    public float range;
    public AOESpell spell;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            damage = (stats.damage.GetValue() + spell.damage) * (stats.damagePercentage.GetValue() / 100 + 1);
        }
        else
        {
            CharacterStats stats = parent.GetComponent<CharacterStats>();
            damage = stats.damage.GetValue();
        }

        speed = new Vector3(spell.speed, spell.speed, 0);
        range = (spell.range/2) + 1;

        startScale = transform.localScale/2;

        SetColour();
    }

    private void Update()
    {
        transform.localScale += (speed * Time.deltaTime)/2;
        float traveled = Vector2.Distance(transform.localScale, startScale);
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
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collision2D>().collider);
        }
    }
}
