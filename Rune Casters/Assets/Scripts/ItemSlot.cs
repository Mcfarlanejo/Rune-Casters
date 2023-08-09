using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemObject item;
    public Sprite image;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponentInChildren<Image>().sprite;
        button = GetComponent<Button>();

        if (item != null)
        {
            image = item.baseItem.image;
        }
        
        button.onClick.AddListener(ShowItemFocus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ShowItemFocus()
    {
        if (item != null)
        {
            //ItemFocus.instance.gameObject.SetActive(true);
            ItemFocus.instance.ChangeItem(item);
        }
    }
}
