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
        UIManager.instance.UpdateFocus(spell);
    }
}
