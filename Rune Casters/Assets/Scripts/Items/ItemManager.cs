using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneCount
{
    public Rune rune;
    public int count;
}

public class ItemManager : MonoBehaviour
{
    #region Singleton
    public static ItemManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemManager>();
            }
            return _instance;
        }
    }
    static ItemManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public List<ItemObject> equipment =  new List<ItemObject>();
    public List<ItemObject> inventory = new List<ItemObject>();
    public List<RuneCount> runeBag = new List<RuneCount>();

    public int coins = 0;

    // Start is called before the first frame update
    public void AddItem(ItemObject newItem)
    {
        inventory.Add(newItem);
    }

    public void EquipItem(ItemObject item)
    {
        ItemObject oldItem = null;
        foreach (ItemObject i in equipment)
        {
            if (i.equipmentType == item.equipmentType)
            {
                oldItem = i;
            }
        }
        if (oldItem != null)
        {
            equipment.Remove(oldItem);
            inventory.Add(oldItem);
        }

        equipment.Add(item);
        inventory.Remove(item);
        
        PlayerStats.instance.EquipmentChanged(item, oldItem);
    }

    public void AddCoins(int value)
    {
        coins += value;
    }
}
