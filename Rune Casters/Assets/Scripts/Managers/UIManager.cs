using System;
using System.Collections;
using System.Collections.Generic;
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

    public Image projectileOneRarityColour;
    public Image projectileOneImage;

    public Image projectileTwoRarityColour;
    public Image projectileTwoImage;

    public Color[] rarityColours = new Color[5];
    public Color[] elementColours = new Color[4];

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        resumeButton.onClick.AddListener(Resume);
        UpdateButtons();
    }

    private void Update()
    {
        
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
