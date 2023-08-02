using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CraftedSpell : MonoBehaviour
{
    public Spell spellBase;

    private void Start()
    {
        CalculateStats();
    }

    public abstract void CalculateStats();
}
