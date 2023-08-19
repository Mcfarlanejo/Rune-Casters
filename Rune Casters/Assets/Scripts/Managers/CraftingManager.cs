using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RuneOption { Projectile, AOE, Self, Fire, Earth, Water, Wind}

public class CraftingManager : MonoBehaviour
{
    public Button craftingMenuButton;

    public Button[] rarityButtons;
    public TMP_Text[] quantityTexts;

    private int runeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        craftingMenuButton.onClick.AddListener(OnOpen);
        OnOpen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen()
    {
        DisplayCounts();
    }

    private void DisplayCounts()
    {
        int rarityIndex = 0;
        RuneOption runeOption = (RuneOption)runeIndex;

        for (int i = 0; i < quantityTexts.Length; i++)
        {
            if (runeIndex < 3)
            {
                foreach (CastingRuneCount rune in ItemManager.instance.castingRuneBag)
                {
                    if (runeOption.ToString() == rune.rune.castingType.ToString() && (int)rune.rune.rarity == rarityIndex)
                    {
                        quantityTexts[i].text = rune.count.ToString();
                    }
                }
            }
            else if (runeIndex >= 3)
            {
                foreach (ElementalRuneCount rune in ItemManager.instance.elementalRuneBag)
                {
                    if (runeOption.ToString() == rune.rune.element.ToString() && (int)rune.rune.rarity == rarityIndex)
                    {
                        quantityTexts[i].text = rune.count.ToString();
                    }
                }
            }
            rarityIndex++;
        }
    }

    public void ProjectileButton()
    {
        runeIndex = 0;
        DisplayCounts();
    }

    public void AOEButton()
    {
        runeIndex = 1;
        DisplayCounts();
    }

    public void SelfButton()
    {
        runeIndex = 2;
        DisplayCounts();
    }

    public void FireButton()
    {
        runeIndex = 3;
        DisplayCounts();
    }

    public void EarthButton()
    {
        runeIndex = 4;
        DisplayCounts();
    }

    public void WaterButton()
    {
        runeIndex = 5;
        DisplayCounts();
    }

    public void WindButton()
    {
        runeIndex = 6;
        DisplayCounts();
    }
}
