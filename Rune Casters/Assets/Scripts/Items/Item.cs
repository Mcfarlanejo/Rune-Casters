using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Basic, Fire, Earth, Water, Wind }
public enum Rarity { Mundane, Common, Rare, Mystic, Primordial}
public class Item : ScriptableObject
{
    public Sprite image;
    public Element element;
    public Rarity rarity;

    public int buyCost;
    public int sellCost;
}
