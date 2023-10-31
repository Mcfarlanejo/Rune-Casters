using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public Rarity rarity;

    public void DropLoot()
    {
        LootManager.instance.GetItem(rarity, gameObject.transform);
    }
}
