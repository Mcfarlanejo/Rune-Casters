using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class DropTable 
{
    public Rarity rarity;
    public List<Item> items;
}
public class LootManager : MonoBehaviour
{
    #region Singleton
    public static LootManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LootManager>();
            }
            return _instance;
        }
    }
    static LootManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public List<DropTable> dropTables = new List<DropTable>();
    public GameObject itemPrefab;
    public GameObject runeItemPrefab;

    public void GetItem(Rarity r, Transform parent)
    {
        int index = (int)r;
        Debug.Log(index);
        Item i = dropTables[index].items[Random.Range(0, dropTables[index].items.Count)];
        //Item i = dropTables[index].items[2];
        Debug.Log(i.name);
        GameObject p;
        if (i is EquipmentBase)
        {
            p = Instantiate(itemPrefab);
            p.GetComponent<ItemObject>().baseItem = i as EquipmentBase;
            p.GetComponent<ItemPickup>().canPickup = true;
        }
        else
        {
            p = Instantiate(runeItemPrefab);
            p.GetComponent<RunePickup>().rune = i as Rune;
            p.GetComponent<RunePickup>().canPickup = true;
        }
        p.GetComponent<Transform>().position = parent.transform.position;
    }
}
