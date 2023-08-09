using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button quitButton;
    public Button resumeButton;

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        resumeButton.onClick.AddListener(Resume);
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
