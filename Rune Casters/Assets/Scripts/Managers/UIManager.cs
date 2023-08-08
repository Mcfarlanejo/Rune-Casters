using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button quitButton;
    public Button resumeButton;
    public GameObject pauseMenu;
    public GameObject joysticks;

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        resumeButton.onClick.AddListener(Resume);
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);

    }

    private void Quit()
    {
        Application.Quit();
    }
}
