using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPos;
    public float damage;
    public float speed;
    public float range;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetValues()
    {
        speed = 10;
        range = 5;
        startPos = transform.position;

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
