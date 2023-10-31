using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ElementalRuneCount
{
    public Rune rune;
    public int count;
}

[System.Serializable]
public class CastingRuneCount
{
    public CastingRune rune;
    public int count;
}

public class ItemManager : MonoBehaviour
{
    #region Singleton
    public static ItemManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemManager>();
            }
            return _instance;
        }
    }
    static ItemManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public List<ItemObject> equipment =  new List<ItemObject>();
    public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    public List<ItemObject> inventory = new List<ItemObject>();
    public List<ElementalRuneCount> elementalRuneBag = new List<ElementalRuneCount>();
    public List<CastingRuneCount> castingRuneBag = new List<CastingRuneCount>();

    public int coins = 0;

    public TMP_Text hpValue;
    public TMP_Text dmgValue;
    public TMP_Text defValue;
    public TMP_Text castValue;
    public TMP_Text spdValue;

    // Start is called before the first frame update
    public void AddItem(ItemObject newItem)
    {
        inventory.Add(newItem);
    }

    public void EquipItem(ItemObject item)
    {
        ItemObject oldItem = null;
        foreach (ItemObject i in equipment)
        {
            if (i.equipmentType == item.equipmentType)
            {
                oldItem = i;
            }
        }
        if (oldItem != null)
        {
            equipment.Remove(oldItem);
            inventory.Add(oldItem);
        }

        equipment.Add(item);
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (item.equipmentType == slot.type)
            {
                slot.item = item;
                slot.UpdateItem();
            }
        }
        inventory.Remove(item);
        
        PlayerStats.instance.EquipmentChanged(item, oldItem);
        UpdateStatText();
    }

    public void UpdateStatText()
    {
        hpValue.text = PlayerStats.instance.maxHealth.ToString();
        dmgValue.text = Mathf.RoundToInt(PlayerStats.instance.damage.GetValue() * ((float)PlayerStats.instance.damagePercentage.GetValue()/100 + 1)).ToString();
        defValue.text = Mathf.RoundToInt(PlayerStats.instance.defence.GetValue() * ((float)PlayerStats.instance.defencePercentage.GetValue() / 100 + 1)).ToString();
        castValue.text = Mathf.RoundToInt(PlayerStats.instance.castSpeed.GetValue() * ((float)PlayerStats.instance.castSpeedPercentage.GetValue() / 100 + 1)).ToString();
        spdValue.text = (PlayerStats.instance.walkSpeedPercentage.GetValue()).ToString();
    }

    public void AddCoins(int value)
    {
        coins += value;
    }

    internal void AddRune(Rune rune)
    {
        if (rune.element == Element.Basic)
        {
            CastingRune cRune = rune as CastingRune;
            foreach (CastingRuneCount c in castingRuneBag)
            {
                if (cRune.castingType == c.rune.castingType && cRune.rarity == c.rune.rarity)
                {
                    c.count++;
                }
            }
        }
        else
        {
            foreach (ElementalRuneCount r in elementalRuneBag)
            {
                if (rune.element == r.rune.element && rune.rarity == r.rune.rarity)
                {
                    r.count++;
                }
            }
        }
    }
}
