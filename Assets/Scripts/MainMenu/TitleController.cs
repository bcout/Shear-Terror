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

    private GameObject optionsCanvas;
    private GameObject titleCanvas;
    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
        titleCanvas = GameObject.FindGameObjectWithTag("TitleCanvas");
        titleCanvas.SetActive(false);
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
        titleCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Change a given keybind.
    // Takes input of which key to change. 0 is jump, 1 is left, 2 is right.
    // Returns 0 on success, 1 when event is not a key and 2 when key is already in use.
    static int changeKeyBind(int bind)
    {
        Event e = Event.current;
        if(e.isKey){
            if (e.keyCode == GameData.jump_key || e.keyCode == GameData.move_left_key || e.keyCode == GameData.move_right_key)
            {
                Debug.Log(e.keyCode + " is already in use!");
                return 2;
            }

            if (bind == 0)
            {
                GameData.jump_key = e.keyCode;
            }
            else if(bind == 1)
            {
                GameData.move_left_key = e.keyCode;
            }
            else
            {
                GameData.move_right_key = e.keyCode;
            }

            Debug.Log("Detected key code: " + e.keyCode);
            Debug.Log("New keybinds are: ");
            Debug.Log("J: " + GameData.jump_key + "\nL: " + GameData.move_left_key + "\nR" + GameData.move_right_key);

            return 0;
        }
        else{
            return 1;
        }
    }
}
