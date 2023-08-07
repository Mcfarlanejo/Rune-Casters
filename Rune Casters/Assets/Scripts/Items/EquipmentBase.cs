using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Weapon, Helm, Chest, Gloves, Boots}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Item/Equipment")]
public class EquipmentBase : Item
{
    public EquipmentType equipmentType;
    public int defenceMin;
    public int defenceMax;
    public int defencePercentageMin;
    public int defencePercentageMax;
    public int damageMin;
    public int damageMax;
    public int damagePercentageMin;
    public int damagePercentageMax;
    public int castSpeedMin;
    public int castSpeedMax;
    public int castSpeedPercentageMin;
    public int castSpeedPercentageMax;
    public int walkSpeedPercentageMin;
    public int walkSpeedPercentageMax;
}
