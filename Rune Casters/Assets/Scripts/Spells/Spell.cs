using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public List<Rune> elementalRunes = new List<Rune>();
    public List<CastingRune> castingRunes = new List<CastingRune>();
    private void Start()
    {
        CalculateStats();
    }

    public abstract void CalculateStats();
}
