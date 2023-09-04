using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemFocus : MonoBehaviour
{
    #region Singleton
    public static ItemFocus instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemFocus>();
            }
            return _instance;
        }
    }
    static ItemFocus _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public ItemObject item;

    public Image image;
    public TMP_Text itemName;
    public TMP_Text rarityType;
    public TMP_Text primaryStatText;
    public TMP_Text primaryStatValue;
    public TMP_Text secondaryStatText;
    public TMP_Text secondaryStatValue;
    public TMP_Text sellPrice;
    public Button equipButton;
    public InventoryButton iButton;
    // Start is called before the first frame update
    void Start()
    {
        equipButton.onClick.AddListener(Equip);
        equipButton.onClick.AddListener(iButton.LoadInventory);
    }

    private void Equip()
    {

        ItemManager.instance.EquipItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null)
        {
            equipButton.interactable = false;
        }
        else
        {
            equipButton.interactable = true;
        }
    }

    public void ChangeItem(ItemObject newItem)
    {
        item = newItem;

        image.sprite = item.baseItem.image;
        itemName.text = item.baseItem.name;
        rarityType.text = $"{item.rarity} {item.equipmentType}";
        sellPrice.text = $"Sell Price: {item.baseItem.sellCost}";

        switch (item.equipmentType)
        {
            case EquipmentType.Weapon:
                primaryStatText.text = "Attack:";
                primaryStatValue.text = item.damage.ToString();

                secondaryStatText.text = "Cast Speed:";
                secondaryStatValue.text = item.castSpeed.ToString();
                break;
            case EquipmentType.Helm:
                primaryStatText.text = "Defense:";
                primaryStatValue.text = item.defense.ToString();

                secondaryStatText.text = "Damage %:";
                secondaryStatValue.text = item.damagePercentage.ToString() + "%";
                break;
            case EquipmentType.Chest:
                primaryStatText.text = "Defense:";
                primaryStatValue.text = item.defense.ToString();

                secondaryStatText.text = "Defense %:";
                secondaryStatValue.text = item.defensePercentage.ToString() + "%";
                break;
            case EquipmentType.Gloves:
                primaryStatText.text = "Defense:";
                primaryStatValue.text = item.defense.ToString();

                secondaryStatText.text = "Cast Speed %:";
                secondaryStatValue.text = item.castSpeedPercentage.ToString() + "%";
                break;
            case EquipmentType.Boots:
                primaryStatText.text = "Defense:";
                primaryStatValue.text = item.defense.ToString();

                secondaryStatText.text = "Walk Speed %:";
                secondaryStatValue.text = item.walkSpeedPercentage.ToString() + "%";
                break;
            default:
                break;
        }
    }
}
