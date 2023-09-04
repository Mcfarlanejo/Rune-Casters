using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EquipmentSlot : MonoBehaviour
{
    public ItemFocus itemFocus;
    public ItemObject item;
    public Image image;
    public EquipmentType type;

    private void Start()
    {
        itemFocus = ItemFocus.instance;
        GetComponent<Button>().onClick.AddListener(ShowItemFocus);
    }

    private void Update()
    {
        if (item != null)
        {
            image.color = Color.white;
        }
    }

    public void UpdateItem()
    {
        image.sprite = item.baseItem.image;
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
