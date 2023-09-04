using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    static UIManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public Button quitButton;
    public Button resumeButton;
    public Button spellbookButton;

    public Image projectileOneRarityColour;
    public Image projectileOneImage;

    public Image projectileTwoRarityColour;
    public Image projectileTwoImage;

    public Color[] rarityColours = new Color[5];
    public Color[] elementColours = new Color[4];

    public GameObject projectileContainer;
    public GameObject aoeContainer;
    public GameObject selfContainer;
    public GameObject spellbookSlotPrefab;

    public Image spellbookFocusImage;
    public TMP_Text spellbookFocusName;
    public Image spellbookFocusBG;
    

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        resumeButton.onClick.AddListener(Resume);
        spellbookButton.onClick.AddListener(LoadSpellbook);
        UpdateButtons();
    }

    public void LoadSpellbook()
    {
        foreach(Transform child in projectileContainer.transform) { Destroy(child.gameObject); }
        foreach(Transform child in aoeContainer.transform) { Destroy(child.gameObject); }
        foreach(Transform child in selfContainer.transform) { Destroy(child.gameObject); }

        foreach (Spell spell in PlayerController.instance.spells)
        {
            switch (spell.castingRunes[0].castingType)
            {
                case CastingType.Projectile:
                    AddSpellToSpellbook(spell, projectileContainer);
                    break;
                case CastingType.AOE:
                    AddSpellToSpellbook(spell, aoeContainer);
                    break;
                case CastingType.Self:
                    AddSpellToSpellbook(spell, selfContainer);
                    break;
                default:
                    break;
            }
        }
        projectileContainer.GetComponentsInChildren<SpellbookSlot>()[0].Focus();
    }

    private void AddSpellToSpellbook(Spell spell, GameObject parent)
    {
        GameObject newSpellslot = Instantiate(spellbookSlotPrefab, parent.transform);
        newSpellslot.GetComponent<SpellbookSlot>().spell = spell;
        newSpellslot.GetComponent<Image>().color = rarityColours[(int)spell.rarity];
        newSpellslot.GetComponentsInChildren<Image>()[1].sprite = spell.elementalRunes[0].image;
        newSpellslot.GetComponentInChildren<TMP_Text>().text = $"{spell.rarity} {spell.element}";
    }

    public void UpdateButtons()
    {
        projectileOneRarityColour.color = rarityColours[(int)PlayerController.instance.projectileOne.rarity];
        projectileOneImage.color = elementColours[(int)PlayerController.instance.projectileOne.element - 1];

        if (PlayerController.instance.projectileTwo != null)
        {
            projectileTwoRarityColour.color = rarityColours[(int)PlayerController.instance.projectileTwo.rarity];
            projectileTwoImage.color = elementColours[(int)PlayerController.instance.projectileTwo.element - 1];
        }
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
    }

    private void Quit()
    {
        Application.Quit();
    }
}
