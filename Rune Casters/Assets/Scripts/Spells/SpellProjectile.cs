using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spell", menuName = "Spell/Projectile")]
public class SpellProjectile : Spell
{
    public int damageMin;
    public int damageMax;

    public int rangeMin;
    public int rangeMax;

    public int speedMin;
    public int speedMax;

    public int castDelayMin;
    public int castDelayMax;
}
