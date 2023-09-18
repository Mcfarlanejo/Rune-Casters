using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookSlot : MonoBehaviour
{
    public Spell spell;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Focus);
    }

    public void Focus()
    {
        UIManager.instance.spellbookFocusImage.sprite = spell.elementalRunes[0].image;
        UIManager.instance.spellbookFocusBG.color = UIManager.instance.rarityColours[(int)spell.rarity];
        UIManager.instance.spellbookFocusName.text = $"{spell.rarity} {spell.element} {spell.castingRunes[0].castingType}";

        if (spell.castingRunes[0].castingType == CastingType.Self)
        {
            SelfSpell s = (SelfSpell)spell;
            if (s.damage != 0)
            {
                UIManager.instance.text1.text = "Damage:";
                UIManager.instance.value1.text = s.damage.ToString();
            }
            else if(s.defence != 0)
            {
                UIManager.instance.text1.text = "Defense:";
                UIManager.instance.value1.text = s.defence.ToString();
            }
            else if (s.speed != 0)
            {
                UIManager.instance.text1.text = "Speed:";
                UIManager.instance.value1.text = s.speed.ToString();
            }
            else
            {
                UIManager.instance.text1.text = "Cast Speed:";
                UIManager.instance.value1.text = s.castSpeed.ToString();
            }

            UIManager.instance.text2.text = "";
            UIManager.instance.value2.text = "";
            UIManager.instance.text3.text = "";
            UIManager.instance.value3.text = "";

            UIManager.instance.value4.text = s.castDelay.ToString();
        }
        else
        {
            UIManager.instance.text1.text = "Damage:";
            UIManager.instance.text2.text = "Range:";
            UIManager.instance.text3.text = "Speed:";

            if (spell.castingRunes[0].castingType == CastingType.Projectile)
            {
                ProjectileSpell s = (ProjectileSpell)spell;
                UIManager.instance.value1.text = s.damage.ToString();
                UIManager.instance.value2.text = s.range.ToString();
                UIManager.instance.value3.text = s.speed.ToString();
                UIManager.instance.value4.text = s.castDelay.ToString();
            }
            else
            {
                AOESpell s = (AOESpell)spell;
                UIManager.instance.value1.text = s.damage.ToString();
                UIManager.instance.value2.text = s.range.ToString();
                UIManager.instance.value3.text = s.speed.ToString();
                UIManager.instance.value4.text = s.castDelay.ToString();
            }
        }

        UIManager.instance.text4.text = "Cast Delay:";
        
    }
}
