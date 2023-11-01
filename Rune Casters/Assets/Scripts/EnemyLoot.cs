using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public Rarity rarity;

    public void DropLoot(Transform t)
    {
        LootManager.instance.GetItem(rarity, t);
    }
}
