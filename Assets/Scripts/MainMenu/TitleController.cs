using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    // public Button startButton;
    // public Button optionsButton;
    // public Button quitButton;

    private bool onOptions;
    private GameObject optionsCanvas;
    private GameObject titleCanvas;
    
    public Text miscText;

    private bool waiting;
    private int status;
    private int bind;

    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
        titleCanvas = GameObject.FindGameObjectWithTag("TitleCanvas");
        titleCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        onOptions = false;
        miscText.text = "Select the keybind to change";
        waiting = false;
        status = -1;
        bind = -1;
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
        onOptions = true;
        titleCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }
    


    private void handleKeyErrors()
    {
        if (status == 1)
        {
            miscText.text = "Key already in use. Try again.";
        }
        else if (status == 0)
        {
            miscText.text = "Successfully changed key bind";
        }
    }

    public void changeJmp()
    {
        if (!waiting)
        {
            bind = 0;
            changeKeyBind();
        }
    }
    
    public void changeLeft()
    {
        if (!waiting)
        {
            bind = 1;
            changeKeyBind();
        }
    }
    
    public void changeRight()
    {
        if (!waiting)
        {
            bind = 2;
            changeKeyBind();
        }
    }
    
    public void changeSpin()
    {
        if (!waiting)
        {
            bind = 3;
            changeKeyBind();
        }
    }
    
    public void goBack()
    {
        // TODO: figure out what to do here.
        Debug.Log("Back pressed.");
        onOptions = false;
        optionsCanvas.SetActive(false);
        titleCanvas.SetActive(true);
    }
    
    void OnGUI()
    {
        if (onOptions)
        {
            Event e = Event.current;
            if (waiting && e.isKey && e.type == EventType.KeyUp)
            {
                if (e.keyCode == GameData.jump_key || e.keyCode == GameData.move_left_key ||
                    e.keyCode == GameData.move_right_key || e.keyCode == GameData.spin_key)
                {
                    miscText.text = "Key already in use. Try again.";
                }
                else
                {

                    if (bind == 0)
                    {
                        GameData.jump_key = e.keyCode;
                    }
                    else if (bind == 1)
                    {
                        GameData.move_left_key = e.keyCode;
                    }
                    else if (bind == 2)
                    {
                        GameData.move_right_key = e.keyCode;
                    }
                    else if (bind == 3)
                    {
                        GameData.spin_key = e.keyCode;
                    }

                    waiting = false;
                    bind = -1;
                    miscText.text = "Successfully changed key bind";

                    // Debug.Log("Detected key code: " + e.keyCode);
                    // Debug.Log("New keybinds are: ");
                    // Debug.Log("J: " + GameData.jump_key + "\tL: " + GameData.move_left_key + "\tR" + GameData.move_right_key + "\tS" + GameData.spin_key);
                }
            }
        }
    }
    
    // Change a given keybind.
    // Takes input of which key to change. 0 is jump, 1 is left, 2 is right.
    // Returns 0 on success, 1 when event is not a key and 2 when key is already in use.
    void changeKeyBind()
    {
        miscText.text = "Press the key to change to";
        waiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
