using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Rune[] elementalRunes = new Rune[10];
    public CastingRune[] castingRunes = new CastingRune[10];

    public Element element;
    public Rarity rarity;
    private void Start()
    {
        CalculateStats();
    }

    public abstract void CalculateStats();
}
