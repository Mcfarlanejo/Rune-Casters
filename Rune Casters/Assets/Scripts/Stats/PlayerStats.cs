using System.Collections;
using System.Collections.Generic;
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

    public override void Die()
    {
        //Do Death stuff
    }
}
