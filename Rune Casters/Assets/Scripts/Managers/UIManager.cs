using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button quitButton;
    public Button resumeButton;

    public Button projectileOneButton;
    public Image projectileOneRarityColour;
    public Image projectileOneImage;

    public Button projectileTwoButton;
    public Image projectileTwoRarityColour;
    public Image projectileTwoImage;

    public Color[] rarityColours = new Color[5];

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        resumeButton.onClick.AddListener(Resume);
    }

    private void Update()
    {
        if (PlayerController.instance.projectileTwo == null)
        {
            projectileTwoButton.enabled = false;
        }
        else
        {
            projectileTwoButton.enabled = true;
            projectileTwoRarityColour.color = rarityColours[(int)PlayerController.instance.projectileTwo.rarity - 1];
            projectileTwoImage.sprite = PlayerController.instance.projectilePrefab.GetComponent<SpriteRenderer>().sprite;
        }

        projectileOneRarityColour.color = rarityColours[(int)PlayerController.instance.projectileOne.rarity - 1];
        projectileOneImage.sprite = PlayerController.instance.projectilePrefab.GetComponent<SpriteRenderer>().sprite;



        if (PlayerController.instance.activeProjectile == PlayerController.instance.projectileOne)
        {
            projectileOneButton.GetComponent<Image>().enabled = true;
            projectileTwoButton.GetComponent<Image>().enabled = false;
        }
        else
        {
            projectileOneButton.GetComponent<Image>().enabled = false;
            projectileTwoButton.GetComponent<Image>().enabled = true;
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
