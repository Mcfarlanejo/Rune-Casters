using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemObject item;
    public GameObject itemsParent;

    private void Start()
    {
        item = GetComponent<ItemObject>();
        itemsParent = GameObject.Find("ItemsParent");
    }
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        ItemManager.instance.AddItem(item);
        transform.SetParent(itemsParent.transform);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Debug.Log(item.name);
    }
}
