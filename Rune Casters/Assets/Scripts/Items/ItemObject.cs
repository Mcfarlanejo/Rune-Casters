using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public EquipmentBase baseItem;
    public EquipmentType equipmentType;
    public Element element;
    public Rarity rarity;

    public int buyCost;
    public int sellCost;
    private System.Random rand = new System.Random();

    public int damage;
    public int damagePercentage;
    public int defense;
    public int defensePercentage;
    public int castSpeed;
    public int castSpeedPercentage;
    public int walkSpeedPercentage;
    // Start is called before the first frame update
    void Start()
    {
        AssignValues();
    }

    private void AssignValues()
    {
        GetComponent<SpriteRenderer>().sprite = baseItem.image;
        equipmentType = baseItem.equipmentType;
        element = baseItem.element;
        rarity = baseItem.rarity;
        buyCost = baseItem.buyCost;
        sellCost = baseItem.sellCost;

        damage = rand.Next(baseItem.damageMin, baseItem.damageMax + 1);
        damagePercentage = rand.Next(baseItem.damagePercentageMin, baseItem.damagePercentageMax + 1);
        defense = rand.Next(baseItem.defenceMin, baseItem.defenceMax + 1);
        defensePercentage = rand.Next(baseItem.defencePercentageMin, baseItem.defencePercentageMax + 1);
        castSpeed = rand.Next(baseItem.castSpeedMin, baseItem.castSpeedMax + 1);
        castSpeedPercentage = rand.Next(baseItem.castSpeedPercentageMin, baseItem.castSpeedPercentageMax + 1);
        walkSpeedPercentage = rand.Next(baseItem.walkSpeedPercentageMin, baseItem.castSpeedPercentageMax + 1);
    }
}
