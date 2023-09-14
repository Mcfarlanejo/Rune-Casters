using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSpell : Spell
{
    //Fire
    public int damage = 0;
    public int damageMin = 1;
    public int damageMax = 10;

    //Earth
    public int defence = 0;
    public int defenceMin = 5;
    public int defenceMax = 10;

    //Wind
    public int speed = 0;
    public int speedMin = 1;
    public int speedMax = 10;

    //Water
    public int castSpeed = 0;
    public int castSpeedMin = 1;
    public int castSpeedMax = 10;

    //Casting
    public int castDelay = 0;
    public int castDelayMin = 1;
    public int castDelayMax = 10;

    public override void CalculateStats()
    {
        element = elementalRunes[0].element;
        rarity = elementalRunes[0].rarity;

        switch (element)
        {
            case Element.Fire:
                CalculateStat(elementalRunes, ref damage, damageMin, damageMax);
                break;
            case Element.Earth:
                CalculateStat(elementalRunes, ref defence, defenceMin, defenceMax);
                break;
            case Element.Water:
                CalculateStat(elementalRunes, ref castSpeed, castSpeedMin, castSpeedMax);
                break;
            case Element.Wind:
                CalculateStat(elementalRunes, ref speed, speedMin, speedMax);
                break;
        }

        CalculateStat(castingRunes, ref castDelay, castDelayMin, castDelayMax);
    }
}
