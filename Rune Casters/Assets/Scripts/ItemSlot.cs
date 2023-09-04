using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemFocus itemFocus;
    public ItemObject item;
    public Image image;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        itemFocus = ItemFocus.instance;
        //image = gameObject.GetComponentInChildren<Image>();
        button = GetComponent<Button>();

        if (item != null)
        {
            image.sprite = item.baseItem.image;
        }
        
        button.onClick.AddListener(ShowItemFocus);
    }

    private void Update()
    {
        if (item != null)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = new Color(0, 0, 0, 0);
        }
    }

    public void UpdateItem()
    {
        if (item != null)
        {
            image.sprite = item.baseItem.image;
        }
        else
        {
            image = null;
        }
        
    }
    
    private void ShowItemFocus()
    {
        if (item != null)
        {
            //ItemFocus.instance.gameObject.SetActive(true);
            itemFocus.ChangeItem(item);
        }
    }
}
