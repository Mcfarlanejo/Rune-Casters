using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
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
    public Rarity eRarity;
    public TMP_Text elementCount;
    private int[] currentElementTotals = new int[5]; 

    public Image castingImage;
    public TMP_Text castingText;
    public TMP_Text castingRarity;
    public Rarity cRarity;
    public TMP_Text castingCount;
    private int[] currentCastingTotals = new int[5];

    public Image resultImage;
    public TMP_Text resultText;
    public TMP_Text resultRarity;
    public GameObject spellHolder;
    public GameObject spellBook;
    public CastingType resultingCastingType;
    public Element resultingElement;
    public Rarity resultingRarity;
    public Sprite projectileSprite;
    public Sprite selfSprite;
    public Sprite AOESprite;

    public GameObject cross;
    public GameObject plus;
    public Button createButton;

    public List<Rune> elementalRunes = new List<Rune>();
    public List<CastingRune> castingRunes = new List<CastingRune>();

    private int runeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        craftingMenuButton.onClick.AddListener(OnOpen);
        OnOpen();
        LoadRunes();
    }

    private void LoadRunes()
    {
        castingRunes = Resources.LoadAll("Runes", typeof(CastingRune)).Cast<CastingRune>().ToList();
        List<Rune> temp = Resources.LoadAll("Runes", typeof(Rune)).Cast<Rune>().ToList();

        
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] is not CastingRune)
            {
                elementalRunes.Add(temp[i]);
            }
        }

        Debug.Log("Casting: " + castingRunes.Count);
        Debug.Log("Elemental: " + elementalRunes.Count);
    }

    // Update is called once per frame
    void Update()
    {
        switch (resultingCastingType)
        {
            case CastingType.Projectile:
                resultImage.sprite = projectileSprite;
                break;
            case CastingType.AOE:
                resultImage.sprite = AOESprite;
                break;
            case CastingType.Self:
                resultImage.sprite = selfSprite;
                break;
            default:
                break;
        }

        switch (resultingElement)
        {
            case Element.Fire:
                resultImage.color = Color.red;
                break;
            case Element.Earth:
                resultImage.color = new Color32(154, 73, 0, 255);
                break;
            case Element.Water:
                resultImage.color = Color.blue;
                break;
            case Element.Wind:
                resultImage.color = Color.green;
                break;
            default:
                break;
        }

        if (!CheckForValidCraft())
        {
            createButton.interactable = false;
        }
        else
        {
            createButton.interactable = true;
        }
    }

    public void OnOpen()
    {
        ProjectileButton();
    }

    public void Craft()
    {
        GameObject newSpell = Instantiate(spellHolder, spellBook.transform);

        switch (resultingCastingType)
        {
            case CastingType.Projectile:
                ProjectileSpell newProjectile = newSpell.AddComponent<ProjectileSpell>();
                AddRunes(newProjectile);
                newProjectile.CalculateStats();
                break;
            case CastingType.AOE:
                AOESpell newAOE = newSpell.AddComponent<AOESpell>();
                AddRunes(newAOE);
                newAOE.CalculateStats();
                break;
            case CastingType.Self:
                SelfSpell newSelf = newSpell.AddComponent<SelfSpell>();
                AddRunes(newSelf);
                newSelf.CalculateStats();
                break;
            default:
                break;
        }

        foreach (ElementalRuneCount rune in ItemManager.instance.elementalRuneBag)
        {
            if (rune.rune.element == resultingElement && rune.rune.rarity == resultingRarity)
            {
                rune.count -= Convert.ToInt32(elementCount.text);
            }
        }
        foreach (CastingRuneCount rune in ItemManager.instance.castingRuneBag)
        {
            if (rune.rune.castingType == resultingCastingType && rune.rune.rarity == resultingRarity)
            {
                rune.count -= Convert.ToInt32(castingCount.text);
            }
        }

        if (runeIndex < 3)
        {
            currentElementTotals[(int)resultingRarity] -= Convert.ToInt32(elementCount.text);
        }
        else
        {
            currentCastingTotals[(int)resultingRarity] -= Convert.ToInt32(castingCount.text);
        }
        DisplayCounts();
        UpdateDisplay();

        elementCount.text = "0";
        castingCount.text = "0";

        PlayerController.instance.spells.Add(newSpell.GetComponent<Spell>());
        if (PlayerController.instance.projectileTwo == null && resultingCastingType == CastingType.Projectile)
        {
            PlayerController.instance.projectileTwo = (ProjectileSpell)newSpell.GetComponent<Spell>();
        }
        else if (PlayerController.instance.activeAOE == null && resultingCastingType == CastingType.AOE)
        {
            PlayerController.instance.activeAOE = (AOESpell)newSpell.GetComponent<Spell>();
        }
        else if (PlayerController.instance.activeSelfSpell == null && resultingCastingType == CastingType.Self)
        {
            PlayerController.instance.activeSelfSpell = (SelfSpell)newSpell.GetComponent<Spell>();
        }

        UIManager.instance.UpdateButtons();
    }

    private void AddRunes(Spell spell)
    {
        var elementalRune = elementalRunes.Find(x => x.element == resultingElement && x.rarity == resultingRarity);
        
        for (int i = 0; i < Convert.ToInt32(elementCount.text); i++)
        {
            spell.elementalRunes[i] = elementalRune;
        }

        var castingRune = castingRunes.Find(x => x.castingType == resultingCastingType && x.rarity == resultingRarity);

        for (int i = 0; i < Convert.ToInt32(castingCount.text); i++)
        {
            spell.castingRunes[i] = castingRune;
        }
    }

    private bool CheckForValidCraft()
    {
        if (!cross.activeInHierarchy && elementCount.text != "-" && elementCount.text != "0"
             && castingCount.text != "-" && castingCount.text != "0")
        {
            return true;
        }
        return false;
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
                        currentCastingTotals[i] = rune.count;
                        castingImage.sprite = rune.rune.image;
                        castingText.text = runeOption.ToString();
                        resultingCastingType = rune.rune.castingType;

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
                        currentElementTotals[i] = rune.count;
                        elementImage.sprite = rune.rune.image;
                        elementText.text = runeOption.ToString();
                        resultingElement = rune.rune.element;

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
        if (runeIndex < 3 )
        {
            castingRarity.text = rarity.ToString();
            cRarity = rarity;
            castingCount.text = "0";
        }
        else
        {
            elementRarity.text = rarity.ToString();
            eRarity = rarity;
            elementCount.text = "0";
        }

        if (castingRarity.text == elementRarity.text)
        {
            resultingRarity = rarity;
            cross.SetActive(false);
            plus.SetActive(true);
        }
        else if (castingRarity.text != "-" && elementRarity.text != "-")
        {
            Debug.Log("Rarity must match");
            cross.SetActive(true);
            plus.SetActive(false);
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
        if (Convert.ToUInt32(elementCount.text) < 10 && currentElementTotals[(int)eRarity] >= Convert.ToInt32(elementCount.text) + 1 && !cross.activeInHierarchy)
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
        if (Convert.ToUInt32(castingCount.text) < 10 && currentCastingTotals[(int)cRarity] >= Convert.ToInt32(castingCount.text) + 1 && !cross.activeInHierarchy)
        {
            castingCount.text = (Convert.ToInt32(castingCount.text) + 1).ToString();
        }
    }

    public void ProjectileButton()
    {
        runeIndex = 0;
        DisplayCounts();
        UpdateDisplay();
        castingRarity.text = "-";
        castingCount.text = "0";
    }

    public void AOEButton()
    {
        runeIndex = 1;
        DisplayCounts();
        UpdateDisplay();
        castingRarity.text = "-";
        castingCount.text = "0";
    }

    public void SelfButton()
    {
        runeIndex = 2;
        DisplayCounts();
        UpdateDisplay();
        castingRarity.text = "-";
        castingCount.text = "0";
    }

    public void FireButton()
    {
        runeIndex = 3;
        DisplayCounts();
        UpdateDisplay();
        elementRarity.text = "-";
        elementCount.text = "0";
    }

    public void EarthButton()
    {
        runeIndex = 4;
        DisplayCounts();
        UpdateDisplay();
        elementRarity.text = "-";
        elementCount.text = "0";
    }

    public void WaterButton()
    {
        runeIndex = 5;
        DisplayCounts();
        UpdateDisplay();
        elementRarity.text = "-";
        elementCount.text = "0";
    }

    public void WindButton()
    {
        runeIndex = 6;
        DisplayCounts();
        UpdateDisplay();
        elementRarity.text = "-";
        elementCount.text = "0";
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
