using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startGame()
    {
        Debug.Log("Start button pressed");
    }

    public void endGame()
    {
        Debug.Log("Exit button pressed");
        Application.Quit();
    }

    public void launchOptions()
    {
        Debug.Log("Options button pressed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
