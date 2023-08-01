using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Basic, Fire, Earth, Water, Wind}
public class EquipmentBase : ScriptableObject
{
    public int buyCost;
    public int sellCost;
    public Element element;
}
