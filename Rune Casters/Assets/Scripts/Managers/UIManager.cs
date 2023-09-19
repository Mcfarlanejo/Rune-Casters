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

    public Image AOERarityColour;
    public Image AOEImage;

    public Image selfRarityColour;
    public Image selfImage;

    public Color[] rarityColours = new Color[5];
    public Color[] elementColours = new Color[4];

    public GameObject projectileContainer;
    public GameObject aoeContainer;
    public GameObject selfContainer;
    public GameObject spellbookSlotPrefab;

    public Image spellbookFocusImage;
    public TMP_Text spellbookFocusName;
    public Image spellbookFocusBG;
    public Button equipSpellButton;
    public Button equipProjectile2Button;
    public Spell focusedSpell;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;

    public TMP_Text value1;
    public TMP_Text value2;
    public TMP_Text value3;
    public TMP_Text value4;
    

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        resumeButton.onClick.AddListener(Resume);
        spellbookButton.onClick.AddListener(LoadSpellbook);
        UpdateButtons();
    }

    private void Update()
    {
        if (PlayerController.instance.aoeButton.interactable == false)
        {
            Color old = AOERarityColour.color;
            Color temp = new Color(old.r, old.g, old.b, .42f);
            AOERarityColour.color = temp;

            old = AOEImage.color;
            temp = new Color(old.r, old.g, old.b, .42f);
            AOEImage.color = temp;
        }
        else
        {
            Color old = AOERarityColour.color;
            Color temp = new Color(old.r, old.g, old.b, 1f);
            AOERarityColour.color = temp;

            old = AOEImage.color;
            temp = new Color(old.r, old.g, old.b, 1f);
            AOEImage.color = temp;
        }

        if (PlayerController.instance.selfButton.interactable == false)
        {
            Color old = selfRarityColour.color;
            Color temp = new Color(old.r, old.g, old.b, .42f);
            selfRarityColour.color = temp;

            old = selfImage.color;
            temp = new Color(old.r, old.g, old.b, .42f);
            selfImage.color = temp;
        }
        else
        {
            Color old = selfRarityColour.color;
            Color temp = new Color(old.r, old.g, old.b, 1f);
            selfRarityColour.color = temp;

            old = selfImage.color;
            temp = new Color(old.r, old.g, old.b, 1f);
            selfImage.color = temp;
        }
    }

    public void LoadSpellbook()
    {
        foreach(Transform child in projectileContainer.transform) { Destroy(child.gameObject); }
        foreach(Transform child in aoeContainer.transform) { Destroy(child.gameObject); }
        foreach(Transform child in selfContainer.transform) { Destroy(child.gameObject); }

        foreach (Spell spell in PlayerController.instance.spells)
        {
            Debug.Log(spell.name);
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

        if (PlayerController.instance.activeAOE != null)
        {
            AOERarityColour.color = rarityColours[(int)PlayerController.instance.activeAOE.rarity];
            AOEImage.color = elementColours[(int)PlayerController.instance.activeAOE.element - 1];
        }

        if (PlayerController.instance.activeSelfSpell != null)
        {
            selfRarityColour.color = rarityColours[(int)PlayerController.instance.activeSelfSpell.rarity];
            selfImage.color = elementColours[(int)PlayerController.instance.activeSelfSpell.element - 1];
        }
    }

    public void UpdateFocus(Spell spell)
    {
        spellbookFocusImage.sprite = spell.elementalRunes[0].image;
        spellbookFocusBG.color = rarityColours[(int)spell.rarity];
        spellbookFocusName.text = $"{spell.rarity} {spell.element} {spell.castingRunes[0].castingType}";

        focusedSpell = spell;

        if (spell.castingRunes[0].castingType == CastingType.Self)
        {
            SelfSpell s = (SelfSpell)spell;
            if (s.damage != 0)
            {
                text1.text = "Damage:";
                value1.text = s.damage.ToString();
                Debug.Log(s.damage + " VS " + PlayerController.instance.activeSelfSpell.damage);
                if (s.damage < PlayerController.instance.activeSelfSpell.damage)
                {
                    text1.color = Color.red;
                }
                else if (s.damage > PlayerController.instance.activeSelfSpell.damage)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }
            }
            else if (s.defence != 0)
            {
                text1.text = "Defense:";
                value1.text = s.defence.ToString();
                Debug.Log(s.defence + " VS " + PlayerController.instance.activeSelfSpell.defence);
                if (s.defence < PlayerController.instance.activeSelfSpell.defence)
                {
                    text1.color = Color.red;
                }
                else if (s.defence > PlayerController.instance.activeSelfSpell.defence)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }
            }
            else if (s.speed != 0)
            {
                text1.text = "Speed:";
                value1.text = s.speed.ToString();
                Debug.Log(s.speed + " VS " + PlayerController.instance.activeSelfSpell.speed);
                if (s.speed < PlayerController.instance.activeSelfSpell.speed)
                {
                    text1.color = Color.red;
                }
                else if (s.speed > PlayerController.instance.activeSelfSpell.speed)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }
            }
            else if (s.castSpeed != 0)
            {
                text1.text = "Projectile Speed:";
                value1.text = s.castSpeed.ToString();
                Debug.Log(s.castSpeed + " VS " + PlayerController.instance.activeSelfSpell.castSpeed);
                if (s.castSpeed < PlayerController.instance.activeSelfSpell.castSpeed)
                {
                    text1.color = Color.red;
                }
                else if (s.castSpeed > PlayerController.instance.activeSelfSpell.castSpeed)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }
            }

            text2.text = "";
            value2.text = "";
            text3.text = "";
            value3.text = "";

            value4.text = s.castDelay.ToString();
            if (s.castDelay < PlayerController.instance.activeSelfSpell.castDelay)
            {
                text1.color = Color.red;
            }
            else if (s.castDelay > PlayerController.instance.activeSelfSpell.castDelay)
            {
                text1.color = Color.green;
            }
            else
            {
                text1.color = Color.white;
            }
        }
        else
        {
            text1.text = "Damage:";
            text2.text = "Range:";
            text3.text = "Speed:";

            if (spell.castingRunes[0].castingType == CastingType.Projectile)
            {
                ProjectileSpell s = (ProjectileSpell)spell;

                int projectileTwoDamage = PlayerController.instance.projectileTwo != null ? PlayerController.instance.projectileTwo.damage : 0;
                int projectileTwoRange = PlayerController.instance.projectileTwo != null ? PlayerController.instance.projectileTwo.range : 0;
                int projectileTwoSpeed = PlayerController.instance.projectileTwo != null ? PlayerController.instance.projectileTwo.speed : 0;
                int projectileTwoCastDelay = PlayerController.instance.projectileTwo != null ? PlayerController.instance.projectileTwo.castDelay : 0;

                value1.text = s.damage.ToString();
                if (s.damage < Mathf.Min(PlayerController.instance.projectileOne.damage, projectileTwoDamage))
                {
                    text1.color = Color.red;
                }
                else if (s.damage > Mathf.Min(PlayerController.instance.projectileOne.damage, projectileTwoDamage))
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }

                value2.text = s.range.ToString();
                if (s.range < Mathf.Min(PlayerController.instance.projectileOne.range, projectileTwoRange))
                {
                    text1.color = Color.red;
                }
                else if (s.range > Mathf.Min(PlayerController.instance.projectileOne.range, projectileTwoRange))
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }

                value3.text = s.speed.ToString();
                if (s.speed < Mathf.Min(PlayerController.instance.projectileOne.speed, projectileTwoSpeed))
                {
                    text1.color = Color.red;
                }
                else if (s.speed > Mathf.Min(PlayerController.instance.projectileOne.speed, projectileTwoSpeed))
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }

                value4.text = s.castDelay.ToString();
                if (s.castDelay < Mathf.Min(PlayerController.instance.projectileOne.castDelay, projectileTwoCastDelay))
                {
                    text1.color = Color.red;
                }
                else if (s.castDelay > Mathf.Min(PlayerController.instance.projectileOne.castDelay, projectileTwoCastDelay))
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }
            }
            else
            {
                AOESpell s = (AOESpell)spell;
                value1.text = s.damage.ToString();
                if (s.damage < PlayerController.instance.activeAOE.damage)
                {
                    text1.color = Color.red;
                }
                else if (s.damage > PlayerController.instance.activeAOE.damage)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }

                value2.text = s.range.ToString();
                if (s.range < PlayerController.instance.activeAOE.range)
                {
                    text1.color = Color.red;
                }
                else if (s.range > PlayerController.instance.activeAOE.range)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }

                value3.text = s.speed.ToString();
                if (s.speed < PlayerController.instance.activeAOE.speed)
                {
                    text1.color = Color.red;
                }
                else if (s.speed > PlayerController.instance.activeAOE.speed)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }

                value4.text = s.castDelay.ToString();
                if (s.castDelay < PlayerController.instance.activeAOE.castDelay)
                {
                    text1.color = Color.red;
                }
                else if (s.castDelay > PlayerController.instance.activeAOE.castDelay)
                {
                    text1.color = Color.green;
                }
                else
                {
                    text1.color = Color.white;
                }
            }
        }

        text4.text = "Cast Speed:";

        
        UpdateFocusButtons();
    }

    internal void UpdateFocusButtons()
    {
        if (focusedSpell.castingRunes[0].castingType == CastingType.Projectile)
        {
            equipProjectile2Button.gameObject.SetActive(true);
        }
        else
        {
            equipProjectile2Button.gameObject.SetActive(false);
        }
    }

    public void SetFocusedSpell()
    {
        switch (focusedSpell.castingRunes[0].castingType)
        {
            case CastingType.Projectile:
                PlayerController.instance.projectileOne = (ProjectileSpell)focusedSpell;
                break;
            case CastingType.AOE:
                PlayerController.instance.activeAOE = (AOESpell)focusedSpell;
                break;
            case CastingType.Self:
                PlayerController.instance.activeSelfSpell = (SelfSpell)focusedSpell;
                break;
        }

        UpdateButtons();
    }

    public void SetProjectile2()
    {
        PlayerController.instance.projectileTwo = (ProjectileSpell)focusedSpell;
        UpdateButtons();
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
