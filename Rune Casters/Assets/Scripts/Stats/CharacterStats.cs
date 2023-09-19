using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public GameObject damageNumberPrefab;

    public int maxHealth = 50;
    public int currentHealth;

    public Stat damage;
    public Stat damagePercentage;
    public Stat defence;
    public Stat defencePercentage;
    public Stat castSpeed;
    public Stat castSpeedPercentage;
    public Stat walkSpeedPercentage;

    public Slider healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = 1;
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

    public virtual void TakeDamage(int damage)
    {
        damage = damage - defence.GetValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);

        ShowDamageNumber(damage);

        if (currentHealth <= 0)
        {
            Die();
        }

        healthBar.value = (float)currentHealth / (float)maxHealth;
    }

    private void ShowDamageNumber(int damage)
    {
        GameObject damageNumber = Instantiate(damageNumberPrefab);
        damageNumber.transform.position = gameObject.transform.position;
        damageNumber.GetComponent<TMP_Text>().text = damage.ToString();
        damageNumber.GetComponent<DamageNumbers>().parent = gameObject;
    }

    public virtual void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
