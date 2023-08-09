using System.Collections;
using System.Collections.Generic;
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
    public TMPro.TextMeshPro itemName;
    public TMPro.TextMeshPro rarityType;
    public TMPro.TextMeshPro primaryStatText;
    public TMPro.TextMeshPro primaryStatValue;
    public TMPro.TextMeshPro secondaryStatText;
    public TMPro.TextMeshPro secondaryStatValue;
    public TMPro.TextMeshPro sellPrice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeItem(ItemObject newItem)
    {
        item = newItem;

        image.sprite = item.baseItem.image;
        itemName.text = item.name;
        rarityType.text = $"{item.rarity} {item.equipmentType}";

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
