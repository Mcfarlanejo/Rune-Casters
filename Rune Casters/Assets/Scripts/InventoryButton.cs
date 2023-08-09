using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventorySlots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadInventory()
    {
        ItemSlot[] slots = inventorySlots.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < ItemManager.instance.inventory.Count; i++)
        {
            slots[i].item = ItemManager.instance.inventory[i];
            slots[i].UpdateItem();
        }
    }
}
