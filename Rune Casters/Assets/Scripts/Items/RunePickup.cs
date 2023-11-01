using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePickup : Interactable
{
    public Rune rune;
    public SpriteRenderer highlight;

    private bool highlightSet = false;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = rune.image;
    }

    private void Update()
    {
        if (!highlightSet)
        {
            Color temp = UIManager.instance.rarityColours[(int)rune.rarity];
            highlight.color = new Color(temp.r, temp.g, temp.b, 125);
            highlightSet = true;
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (canPickup)
        {
            PickUp();
        }
    }

    void PickUp()
    {
        ItemManager.instance.AddRune(rune);
        Debug.Log(rune.name);
        Destroy(gameObject);
    }
}
