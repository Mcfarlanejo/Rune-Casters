using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public CharacterStats characterStats;
    public float speed;

    public float viewDistance = 5;
    public float distance;

    private bool canDamage = true;

    public bool chase = false;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (distance < viewDistance)
        {
            chase = true;
        }

        if (chase)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0,0, -angle);
            if (player.transform.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canDamage)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(characterStats.damage.GetValue());
            StartCoroutine(DamageDelay());
        }
    }

    private IEnumerator DamageDelay()
    {
        canDamage = false;
        yield return new WaitForSeconds(1);
        canDamage = true;
    }
}
