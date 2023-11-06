using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Button restartButton;
    public Button exitButton;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
        scene = SceneManager.GetActiveScene();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(scene.name);
    }
}
