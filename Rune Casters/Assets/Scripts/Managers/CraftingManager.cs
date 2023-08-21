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

    public Button[] runeButtons;
    public Button[] rarityButtons;
    public TMP_Text[] quantityTexts;

    public Image elementImage;
    public TMP_Text elementText;
    public TMP_Text elementRarity;
    public TMP_Text elementCount;

    public Image castingImage;
    public TMP_Text castingText;
    public TMP_Text castingRarity;
    public TMP_Text castingCount;

    public Image resultImage;
    public TMP_Text resultText;
    public TMP_Text resultRarity;
    public GameObject spellHolder;

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
        ProjectileButton();
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
                        castingImage.sprite = rune.rune.image;
                        castingText.text = runeOption.ToString();

                        if (rune.count == 0)
                        {
                            rarityButtons[i].interactable = false;
                        }
                        else
                        {
                            rarityButtons[i].interactable = true;
                        }
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
                        elementImage.sprite = rune.rune.image;
                        elementText.text = runeOption.ToString();

                        if (rune.count == 0)
                        {
                            rarityButtons[i].interactable = false;
                        }
                        else
                        {
                            rarityButtons[i].interactable = true;
                        }
                    }
                }
            }
            rarityIndex++;
        }
    }

    private void UpdateResults(Rarity rarity)
    {
        if (runeIndex < 3)
        {
            castingRarity.text = rarity.ToString();
        }
        else
        {
            elementRarity.text = rarity.ToString();
        }
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < runeButtons.Length; i++)
        {
            if (i != runeIndex)
            {
                runeButtons[i].GetComponent<Image>().enabled = false;
            }
            else
            {
                runeButtons[i].GetComponent<Image>().enabled = true;
            }
        }
    }

    public void RemoveElement()
    {
        if (Convert.ToUInt32(elementCount.text) > 0)
        {
            elementCount.text = (Convert.ToInt32(elementCount.text)-1).ToString();
        }
        
    }

    public void AddElement()
    {
        if (Convert.ToUInt32(elementCount.text) < 10)
        {
            elementCount.text = (Convert.ToInt32(elementCount.text) + 1).ToString();
        }
    }

    public void RemoveCasting()
    {
        if (Convert.ToUInt32(castingCount.text) > 0)
        {
            castingCount.text = (Convert.ToInt32(castingCount.text) - 1).ToString();
        }
    }

    public void AddCasting()
    {
        if (Convert.ToUInt32(castingCount.text) < 10)
        {
            castingCount.text = (Convert.ToInt32(castingCount.text) + 1).ToString();
        }
    }

    public void ProjectileButton()
    {
        runeIndex = 0;
        DisplayCounts();
        UpdateDisplay();
    }

    public void AOEButton()
    {
        runeIndex = 1;
        DisplayCounts();
        UpdateDisplay();
    }

    public void SelfButton()
    {
        runeIndex = 2;
        DisplayCounts();
        UpdateDisplay();
    }

    public void FireButton()
    {
        runeIndex = 3;
        DisplayCounts();
        UpdateDisplay();
    }

    public void EarthButton()
    {
        runeIndex = 4;
        DisplayCounts();
        UpdateDisplay();
    }

    public void WaterButton()
    {
        runeIndex = 5;
        DisplayCounts();
        UpdateDisplay();
    }

    public void WindButton()
    {
        runeIndex = 6;
        DisplayCounts();
        UpdateDisplay();
    }

    public void MundaneButton()
    {
        UpdateResults(Rarity.Mundane);
    }

    public void CommonButton()
    {
        UpdateResults(Rarity.Common);
    }

    public void RareButton()
    {
        UpdateResults(Rarity.Rare);
    }

    public void MysticButton()
    {
        UpdateResults(Rarity.Mystic);
    }

    public void PrimordialButton()
    {
        UpdateResults(Rarity.Primordial);
    }
}
