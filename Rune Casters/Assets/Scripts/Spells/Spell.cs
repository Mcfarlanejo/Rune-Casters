using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rune { Mundane, Common, Rare, Mystic, Primordial}
public class Spell : ScriptableObject
{
    public Element element;
    public Rune rune;
}
