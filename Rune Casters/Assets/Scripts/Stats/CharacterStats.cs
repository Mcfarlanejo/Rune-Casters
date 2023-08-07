using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    public Stat damage;
    public Stat damagePercentage;
    public Stat defence;
    public Stat defencePercentage;
    public Stat castSpeed;
    public Stat castSpeedPercentage;
    public Stat walkSpeedPercentage;

    private void Start()
    {
        if (gameObject.tag != "Enemy")
        {

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            TakeDamage(damage.GetValue());
        }
    }

    private void TakeDamage(int damage)
    {
        damage = damage - defence.GetValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (gameObject.tag == "Enemy")
        {

        }
    }
}
