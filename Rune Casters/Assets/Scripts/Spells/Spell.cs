using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Spell : MonoBehaviour
{
    public Rune[] elementalRunes = new Rune[10];
    public CastingRune[] castingRunes = new CastingRune[10];

    public Element element;
    public Rarity rarity;

    public abstract void CalculateStats();

    public void CalculateStat(Rune[] runeArray, ref int stat, int statMin, int statMax)
    {
        float chance = 0;
        foreach (var rune in runeArray)
        {
            if (rune != null)
            {
                chance += 0.1f;
            }
        }
        float temp = Random.Range(0, 1f);

        if (chance >= temp)
        {
            stat = statMax;
        }
        else
        {
            stat = Random.Range(statMin, statMax + 1);
        }

        stat *= (int)rarity + 1;
    }
}
