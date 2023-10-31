using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemObject item;
    public GameObject itemsParent;
    public SpriteRenderer highlight;

    private bool highlightSet = false;

    private void Start()
    {
        item = GetComponent<ItemObject>();
        itemsParent = GameObject.Find("ItemsParent");
    }

    private void Update()
    {
        if (!highlightSet)
        {
            Color temp = UIManager.instance.rarityColours[(int)item.rarity];
            highlight.color = new Color(temp.r, temp.g, temp.b, 125);
            highlightSet = true;
        }
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
        Destroy(highlight.gameObject);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Debug.Log(item.name);
    }
}
