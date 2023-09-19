using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    #region Singleton
    public static PlayerStats instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerStats>();
            }
            return _instance;
        }
    }
    static PlayerStats _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public TMP_Text healthText;
    public void EquipmentChanged(ItemObject newItem, ItemObject oldItem)
    {
        if (oldItem != null)
        {
            damage.RemoveModifier(newItem.damage);
            damagePercentage.RemoveModifier(newItem.damagePercentage);
            defence.RemoveModifier(newItem.defense);
            defencePercentage.RemoveModifier(newItem.defensePercentage);
            castSpeed.RemoveModifier(newItem.castSpeed);
            castSpeedPercentage.RemoveModifier(newItem.castSpeedPercentage);
            walkSpeedPercentage.RemoveModifier(newItem.walkSpeedPercentage);
        }
        if (newItem != null)
        {
            damage.AddModifier(newItem.damage);
            damagePercentage.AddModifier(newItem.damagePercentage);
            defence.AddModifier(newItem.defense);
            defencePercentage.AddModifier(newItem.defensePercentage);
            castSpeed.AddModifier(newItem.castSpeed);
            castSpeedPercentage.AddModifier(newItem.castSpeedPercentage);
            walkSpeedPercentage.AddModifier(newItem.walkSpeedPercentage);
        }
    }

    public void AddTempStats(SelfSpell spell)
    {
        damage.AddModifier(spell.damage);
        defence.AddModifier(spell.defence);
        walkSpeedPercentage.AddModifier(spell.speed);
        castSpeed.AddModifier(spell.castSpeed);

        StartCoroutine(TempStatsDelay(spell));
    }

    public IEnumerator TempStatsDelay(SelfSpell spell)
    {
        yield return new WaitForSeconds(spell.castDelay/2);
        RemoveTempStats(spell);
    }

    public void RemoveTempStats(SelfSpell spell)
    {
        damage.RemoveModifier(spell.damage);
        defence.RemoveModifier(spell.defence);
        walkSpeedPercentage.RemoveModifier(spell.speed);
        castSpeed.RemoveModifier(spell.castSpeed);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    public override void Die()
    {
        //Do Death stuff
    }
}
