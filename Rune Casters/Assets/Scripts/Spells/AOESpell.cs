using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpell : Spell
{
    public int damage = 0;
    public int damageMin = 1;
    public int damageMax = 10;

    public int range = 0;
    public int rangeMin = 5;
    public int rangeMax = 10;

    public int speed = 0;
    public int speedMin = 1;
    public int speedMax = 10;

    public int castDelay = 0;
    public int castDelayMin = 1;
    public int castDelayMax = 10;

    public override void CalculateStats()
    {
        element = elementalRunes[0].element;
        rarity = elementalRunes[0].rarity;

        CalculateStat(elementalRunes, ref damage, damageMin, damageMax);
        CalculateStat(castingRunes, ref range, rangeMin, rangeMax);
        CalculateStat(elementalRunes, ref speed, speedMin, speedMax);
        CalculateStat(castingRunes, ref castDelay, castDelayMin, castDelayMax);
    }
}
