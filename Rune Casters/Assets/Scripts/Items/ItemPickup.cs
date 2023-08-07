using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemObject item;

    private void Start()
    {
        item = GetComponent<ItemObject>();
    }
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        ItemManager.instance.AddItem(item);
        Debug.Log(item.name);
    }
}
