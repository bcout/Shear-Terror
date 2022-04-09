using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    private GameObject optionsCanvas;
    private GameObject optionsCanvasTricks;
    private GameObject keyboardCanvas;
    private GameObject titleCanvas;
    
    public Text miscText;
    public Text miscTextTricks;

    private bool waiting;
    private int bind;
    private int screenNum;  // 0 = title screen, 1 = first menu, 2 = tricks menu

    public Color jmpColor;
    public Color rightColor;
    public Color leftColor;
    public Color lookBackColor;
    public Color frontFlipColor;
    public Color backFlipColor;
    public Color rollLeftColor;
    public Color rollRightColor;
    public Color spinLeftColor;
    public Color spinRightColor;

    // Filled in by unity.
    public Button[] kbButtons =
    {
        
    };

    public KeyCode[] KeyCodes2 =
    {
        KeyCode.Tilde,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
        KeyCode.Minus,
        KeyCode.Plus,
        KeyCode.Backspace,
        KeyCode.Tab,
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.T,
        KeyCode.Y,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P,
        KeyCode.LeftBracket,
        KeyCode.RightBracket,
        KeyCode.Backslash,
        KeyCode.CapsLock,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.Semicolon,
        KeyCode.Quote,
        KeyCode.Return,
        KeyCode.LeftShift,
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,
        KeyCode.B,
        KeyCode.N,
        KeyCode.M,
        KeyCode.Less,
        KeyCode.Greater,
        KeyCode.Question,
        KeyCode.UpArrow,
        KeyCode.RightShift,
        KeyCode.LeftControl,
        KeyCode.LeftAlt,
        KeyCode.Space,
        KeyCode.RightAlt,
        KeyCode.RightControl,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.PageUp,
        KeyCode.PageDown
    };
    
    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
        optionsCanvasTricks = GameObject.FindGameObjectWithTag("OptionsCanvasTricks");
        keyboardCanvas = GameObject.FindGameObjectWithTag("KeyboardCanvas");
        titleCanvas = GameObject.FindGameObjectWithTag("TitleCanvas");
        titleCanvas.SetActive(true);
        keyboardCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        optionsCanvasTricks.SetActive(false);
        
        screenNum = 0;
        miscText.text = "Select the key bind to change";
        miscTextTricks.text = "Select the key bind to change";
        waiting = false;
        bind = -1;

        for (int i = 0; i < 75; i++)
        {
            int tmp = i;    // I have no idea why, but this is needed and it breaks without it.
            kbButtons[i].onClick.AddListener(() => ButtonClicked(tmp));
        }
        
        ColorUtility.TryParseHtmlString("#92FFA7", out jmpColor);
        ColorUtility.TryParseHtmlString("#FF9697", out rightColor);
        ColorUtility.TryParseHtmlString("#86A0FF", out leftColor);
        ColorUtility.TryParseHtmlString("#FFD092", out lookBackColor);
        ColorUtility.TryParseHtmlString("#FFD092", out frontFlipColor);
        ColorUtility.TryParseHtmlString("#92FFA7", out backFlipColor);
        ColorUtility.TryParseHtmlString("#86A0FF", out rollLeftColor);
        ColorUtility.TryParseHtmlString("#FF9697", out rollRightColor);
        ColorUtility.TryParseHtmlString("#E892FF", out spinLeftColor);
        ColorUtility.TryParseHtmlString("#92FFF3", out spinRightColor);

        GameData.currently_going_to_main_menu = false;
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
        screenNum = 1;
        titleCanvas.SetActive(false);
        keyboardCanvas.SetActive(true);
        optionsCanvas.SetActive(true);
        optionsCanvasTricks.SetActive(false);
        refresh_KB_Highlights();
    }
    
    public void launchOptionsTricks()
    {
        screenNum = 2;
        titleCanvas.SetActive(false);
        keyboardCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        optionsCanvasTricks.SetActive(true);
        refresh_KB_Highlights();
    }

    // Go back to the previous screen.
    public void goBack()
    {
        if (screenNum == 2)
        {
            launchOptions();
            screenNum = 1;
        }
        else
        {
            keyboardCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            optionsCanvasTricks.SetActive(false);
            titleCanvas.SetActive(true);
            screenNum = 0;
        }
    }
    
    // For inoutting inputs using the keyboard
    void OnGUI()
    {
        if (screenNum == 1)
        {
            Event e = Event.current;
            if (waiting && e.isKey && e.type == EventType.KeyUp)
            {
                if (e.keyCode == GameData.jump_key || e.keyCode == GameData.move_left_key ||
                    e.keyCode == GameData.move_right_key || e.keyCode == GameData.look_back_key)
                {
                    miscText.text = "Key already in use. Try again.";
                }
                else
                {
                    Debug.Log("fuck me: " + bind);
                    if (bind == 7)
                    {
                        GameData.move_left_key = e.keyCode;
                    }
                    else if (bind == 8)
                    {
                        GameData.move_right_key = e.keyCode;
                    }
                    else if (bind == 9)
                    {
                        GameData.jump_key = e.keyCode;
                    }
                    else if (bind == 10)
                    {
                        GameData.look_back_key = e.keyCode;
                    }
                    refresh_KB_Highlights();
                    waiting = false;
                    bind = -1;
                    miscText.text = "Successfully changed key bind";

                    // Debug.Log("Detected key code: " + e.keyCode);
                    // Debug.Log("New keybinds are: ");
                    // Debug.Log("J: " + GameData.jump_key + "\tL: " + GameData.move_left_key + "\tR" + GameData.move_right_key + "\tS" + GameData.spin_key);
                }
            }
        }
        else if (screenNum == 2)
        {
            Event e = Event.current;
            if (waiting && e.isKey && e.type == EventType.KeyUp)
            {
                if (e.keyCode == GameData.flip_front_key || e.keyCode == GameData.flip_back_key || e.keyCode == GameData.roll_left_key || e.keyCode == GameData.roll_right_key || e.keyCode == GameData.spin_left_key || e.keyCode == GameData.spin_right_key)
                {
                    miscTextTricks.text = "Key already in use. Try again.";
                }
                else
                {
                    if (bind == 1)
                    {
                        GameData.flip_back_key = e.keyCode;
                    }
                    else if (bind == 2)
                    {
                        GameData.roll_right_key = e.keyCode;
                    }
                    else if (bind == 3)
                    {
                        GameData.spin_right_key = e.keyCode;
                    }
                    else if (bind == 4)
                    {
                        GameData.flip_front_key = e.keyCode;
                    }
                    else if (bind == 5)
                    {
                        GameData.roll_left_key = e.keyCode;
                    }
                    else if (bind == 6)
                    {
                        GameData.spin_left_key = e.keyCode;
                    }
                    refresh_KB_Highlights();
                    waiting = false;
                    bind = -1;
                    miscTextTricks.text = "Successfully changed key bind";
        
                    // Debug.Log("Detected key code: " + e.keyCode);
                    // Debug.Log("New keybinds are: ");
                    // Debug.Log("FF: " + GameData.flip_front_key + "\tFB: " + GameData.flip_back_key + "\tRL" + GameData.roll_left_key + "\tRR" + GameData.roll_right_key + "\tSL" + GameData.spin_left_key + "\tSR" + GameData.spin_right_key);
                }
            }
        }
    }
    
    // Tells the listeners that we are waiting for input and to start listening.
    void changeKeyBind()
    {
        if (screenNum == 1)
        {
            miscText.text = "Press the key to change to";
        }
        else if (screenNum == 2)
        {
            miscTextTricks.text = "Press the key to change to";
        }

        waiting = true;
    }

    // Figure out what this button is.
    void handleButtons(int buttonNo)
    {
        int tmp = buttonNo - 62;
        Debug.Log("tmp" + tmp);
        if (tmp == 0 || tmp == 10)
        {
            goBack();
        }

        else if (1 <= tmp && tmp <= 6)
        {
            bind = tmp;
            changeKeyBind();
        }
        else if (tmp == 7)
        {
            launchOptionsTricks();
        }
        else if(tmp == 8 || tmp == 9)
        {
            bind = tmp - 1;
            changeKeyBind();
        }
        else if (tmp == 11 || tmp == 12)
        {
            bind = tmp - 2;
            changeKeyBind();
        }
    }

    // Handle all onscreen button inputs.
    void ButtonClicked(int buttonNo)
    {
        // Debug.Log("Button clicked = " + buttonNo);
        // If button is not from the onscreen keyboard.
        if (buttonNo > 61)
        {
            if (!waiting)
            {
                handleButtons(buttonNo);
            }
        }
        else
        {
            if (waiting)
            {   
                KeyCode k = KeyCodes2[buttonNo];
                if (screenNum == 1)
                {
                    if (k == GameData.jump_key || k == GameData.move_left_key ||
                        k == GameData.move_right_key || k == GameData.look_back_key)
                    {
                        miscText.text = "Key already in use. Try again.";
                    }
                    else
                    {
                        if (bind == 7)
                        {
                            GameData.move_left_key = k;
                        }
                        else if (bind == 8)
                        {
                            GameData.move_right_key = k;
                        }
                        else if (bind == 9)
                        {
                            GameData.jump_key = k;
                        }
                        else if (bind == 10)
                        {
                            GameData.look_back_key = k;
                        }
                        waiting = false;
                        bind = -1;
                        miscText.text = "Successfully changed key bind";
                        refresh_KB_Highlights();
                        // Debug.Log("Detected key code: " + e.keyCode);
                        // Debug.Log("New keybinds are: ");
                        // Debug.Log("J: " + GameData.jump_key + "\tL: " + GameData.move_left_key + "\tR" + GameData.move_right_key + "\tLB" + GameData.look_back_key);
                    }
                }
                else if (screenNum == 2)
                {
                    if (k == GameData.flip_front_key || k == GameData.flip_back_key || k == GameData.roll_left_key || k == GameData.roll_right_key || k == GameData.spin_left_key || k == GameData.spin_right_key)
                    {
                            miscTextTricks.text = "Key already in use. Try again.";
                    }
                    else
                    {
                        if (bind == 1)
                        {
                            GameData.flip_back_key = k;
                        }
                        else if (bind == 2)
                        {
                            GameData.roll_right_key = k;
                        }
                        else if (bind == 3)
                        {
                            GameData.spin_right_key = k;
                        }
                        else if (bind == 4)
                        {
                            GameData.flip_front_key = k;
                        }
                        else if (bind == 5)
                        {
                            GameData.roll_left_key = k;
                        }
                        else if (bind == 6)
                        {
                            GameData.spin_left_key = k;
                        }
                    
                        waiting = false;
                        bind = -1;
                        miscTextTricks.text = "Successfully changed key bind";
                        refresh_KB_Highlights();
                        // Debug.Log("Detected key code: " + e.keyCode);
                        // Debug.Log("New keybinds are: ");
                        // Debug.Log("FF: " + GameData.flip_front_key + "\tFB: " + GameData.flip_back_key + "\tRL" +
                        //           GameData.roll_left_key + "\tRR" + GameData.roll_right_key + "\tSL" +
                        //           GameData.spin_left_key + "\tSR" + GameData.spin_right_key);
                    }
                }
            }
        }
    }

// Refresh the highlighted keys on the keyboard.
    void refresh_KB_Highlights()
    {
        if (screenNum == 2)
        {
            for (int i = 0; i < 62; i++)
            {
                KeyCode k = KeyCodes2[i];
                if (k == GameData.flip_front_key)
                {
                    changeButtonColor(frontFlipColor, kbButtons[i]);
                }
                else if (k == GameData.flip_back_key)
                {
                    changeButtonColor(backFlipColor, kbButtons[i]);
                }
                else if (k == GameData.roll_left_key)
                {
                    changeButtonColor(rollLeftColor, kbButtons[i]);
                }
                else if (k == GameData.roll_right_key)
                {
                    changeButtonColor(rollRightColor, kbButtons[i]);
                }
                else if (k == GameData.spin_left_key)
                {
                    changeButtonColor(spinLeftColor, kbButtons[i]);
                }
                else if (k == GameData.spin_right_key)
                {
                    changeButtonColor(spinRightColor, kbButtons[i]);
                }
                else
                {
                    kbButtons[i].GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                }
            }
        }
        else if (screenNum == 1)
        {
            for (int i = 0; i < 62; i++)
            {
                KeyCode k = KeyCodes2[i];
                if (k == GameData.jump_key)
                {
                    changeButtonColor(jmpColor, kbButtons[i]);
                }
                else if (k == GameData.move_left_key)
                {
                    changeButtonColor(leftColor, kbButtons[i]);
                }
                else if (k == GameData.move_right_key)
                {
                    changeButtonColor(rightColor, kbButtons[i]);
                }
                else if (k == GameData.look_back_key)
                {
                    changeButtonColor(lookBackColor, kbButtons[i]);
                }
                else
                {
                    kbButtons[i].GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                }
            }
        }
    }

    // Changing the colour of a button is ridiculously hard.
    void changeButtonColor(Color col, Button btn)
    {
        ColorBlock colorBlock = btn.GetComponent<Button>().colors;
        colorBlock.normalColor = col;
        colorBlock.highlightedColor = col;
        colorBlock.pressedColor = col;
        colorBlock.selectedColor = col;
        btn.GetComponent<Button>().colors = colorBlock;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
