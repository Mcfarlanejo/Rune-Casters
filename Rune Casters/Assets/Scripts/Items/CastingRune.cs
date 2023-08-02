using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CastingType { Projectile, AOE, Self}
[CreateAssetMenu(fileName = "New Casting Rune", menuName = "Item/Casting Rune")]
public class CastingRune : Rune
{
    public CastingType castingType;
}
