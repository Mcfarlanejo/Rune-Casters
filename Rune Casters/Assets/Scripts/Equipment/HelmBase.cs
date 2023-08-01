using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Helmet", menuName = "Item/Armor/Helmet")]
public class HelmBase : ArmorBase
{
    public int damageIncreasePercentage;
    public int damagePercentageMax;
    public int damagePercentageMin;
}
