using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    // public Button startButton;
    // public Button optionsButton;
    // public Button quitButton;

    private int screenNum;  // 0 = title screen, 1 = first menu, 2 = tricks menu
    private GameObject optionsCanvas;
    private GameObject optionsCanvasTricks;
    private GameObject titleCanvas;
    
    public Text miscText;
    public Text miscTextTricks;

    private bool waiting;
    // private bool needToUpdateKB;
    private int bind;
    // private int kbToUpdate;
    
    // private ColorBlock buttonDefaultColour;

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
        KeyCode.RightArrow
    };
    
    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas");
        optionsCanvasTricks = GameObject.FindGameObjectWithTag("OptionsCanvasTricks");
        titleCanvas = GameObject.FindGameObjectWithTag("TitleCanvas");
        titleCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        optionsCanvasTricks.SetActive(false);
        screenNum = 0;
        miscText.text = "Select the keybind to change";
        waiting = false;
        bind = -1;
        // kbToUpdate = -1;

        for (int i = 0; i < 62; i++)
        {
            int fuck = i;
            kbButtons[i].onClick.AddListener(() => ButtonClicked(fuck));
        }
        
        // buttonDefaultColour = kbButtons[0].GetComponent<Button>().colors;
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
        optionsCanvas.SetActive(true);
        optionsCanvasTricks.SetActive(false);
        refresh_KB_Highlights();
    }
    
    public void launchOptionsTricks()
    {
        screenNum = 2;
        titleCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        optionsCanvasTricks.SetActive(true);
        refresh_KB_Highlights();
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
    
    public void changeLookBack()
    {
        if (!waiting)
        {
            bind = 3;
            changeKeyBind();
        }
    }
    
    public void changeFlipF()
    {
        if (!waiting)
        {
            bind = 4;
            changeKeyBind();
        }
    }
    
    public void changeFlipB()
    {
        if (!waiting)
        {
            bind = 5;
            changeKeyBind();
        }
    }
    
    public void changeRollL()
    {
        if (!waiting)
        {
            bind = 6;
            changeKeyBind();
        }
    }
    
    public void changeRollR()
    {
        if (!waiting)
        {
            bind = 7;
            changeKeyBind();
        }
    }
    
    public void changeSpinL()
    {
        if (!waiting)
        {
            bind = 8;
            changeKeyBind();
        }
    }
    
    public void changeSpinR()
    {
        if (!waiting)
        {
            bind = 9;
            changeKeyBind();
        }
    }

    public void goBack()
    {
        if (screenNum == 2)
        {
            launchOptions();
            screenNum = 1;
        }
        else
        {
            optionsCanvas.SetActive(false);
            optionsCanvasTricks.SetActive(false);
            titleCanvas.SetActive(true);
            screenNum = 0;
        }
    }
    
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
        // else if (screenNum == 2)
        // {
        //     Event e = Event.current;
        //     if (waiting && e.isKey && e.type == EventType.KeyUp)
        //     {
        //         if (e.keyCode == GameData.flip_front_key || e.keyCode == GameData.flip_back_key || e.keyCode == GameData.roll_left_key || e.keyCode == GameData.roll_right_key || e.keyCode == GameData.spin_left_key || e.keyCode == GameData.spin_right_key)
        //         {
        //             miscTextTricks.text = "Key already in use. Try again.";
        //         }
        //         else
        //         {
        //             if (bind == 4)
        //             {
        //                 GameData.flip_front_key = e.keyCode;
        //             }
        //             else if (bind == 5)
        //             {
        //                 GameData.flip_back_key = e.keyCode;
        //             }
        //             else if (bind == 6)
        //             {
        //                 GameData.roll_left_key = e.keyCode;
        //             }
        //             else if (bind == 7)
        //             {
        //                 GameData.roll_right_key = e.keyCode;
        //             }
        //             else if (bind == 8)
        //             {
        //                 GameData.spin_left_key = e.keyCode;
        //             }
        //             else if (bind == 9)
        //             {
        //                 GameData.spin_right_key = e.keyCode;
        //             }
        //             refresh_KB_Highlights();
        //             waiting = false;
        //             bind = -1;
        //             miscTextTricks.text = "Successfully changed key bind";
        //
        //             // Debug.Log("Detected key code: " + e.keyCode);
        //             // Debug.Log("New keybinds are: ");
        //             Debug.Log("FF: " + GameData.flip_front_key + "\tFB: " + GameData.flip_back_key + "\tRL" + GameData.roll_left_key + "\tRR" + GameData.roll_right_key + "\tSL" + GameData.spin_left_key + "\tSR" + GameData.spin_right_key);
        //         }
        //     }
        // }
    }
    
    // Change a given keybind.
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

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
        KeyCode k = KeyCodes2[buttonNo];
        if (waiting)
        {
            waiting = false;
            if (screenNum == 1)
            {
                if (k == GameData.jump_key || k == GameData.move_left_key ||
                    k == GameData.move_right_key || k == GameData.look_back_key)
                {
                    miscText.text = "Key already in use. Try again.";
                }
                else
                {
                    if (bind == 0)
                    {
                        GameData.jump_key = k;
                    }
                    else if (bind == 1)
                    {
                        GameData.move_left_key = k;
                    }
                    else if (bind == 2)
                    {
                        GameData.move_right_key = k;
                    }
                    else if (bind == 3)
                    {
                        GameData.look_back_key = k;
                    }
                    // waiting = false;
                    bind = -1;
                    miscText.text = "Successfully changed key bind";
                    // needToUpdateKB = true;
                    // kbToUpdate = buttonNo;
                    refresh_KB_Highlights();
                    // Debug.Log("Detected key code: " + e.keyCode);
                    // Debug.Log("New keybinds are: ");
                    // Debug.Log("J: " + GameData.jump_key + "\tL: " + GameData.move_left_key + "\tR" + GameData.move_right_key + "\tS" + GameData.spin_key);
                }
            }
            // else if (screenNum == 2)
            // {
            //     if (k == GameData.flip_front_key || k == GameData.flip_back_key || k == GameData.roll_left_key || k == GameData.roll_right_key || k == GameData.spin_left_key || k == GameData.spin_right_key)
            //         {
            //             miscTextTricks.text = "Key already in use. Try again.";
            //         }
                // else
                // {
                //     if (bind == 4)
                //     {
                //         GameData.flip_front_key = k;
                //     }
                //     else if (bind == 5)
                //     {
                //         GameData.flip_back_key = k;
                //     }
                //     else if (bind == 6)
                //     {
                //         GameData.roll_left_key = k;
                //     }
                //     else if (bind == 7)
                //     {
                //         GameData.roll_right_key = k;
                //     }
                //     else if (bind == 8)
                //     {
                //         GameData.spin_left_key = k;
                //     }
                //     else if (bind == 9)
                //     {
                //         GameData.spin_right_key = k;
                //     }
                //
                //     waiting = false;
                //     bind = -1;
                //     miscTextTricks.text = "Successfully changed key bind";
                //     refresh_KB_Highlights();
                //     // Debug.Log("Detected key code: " + e.keyCode);
                //     // Debug.Log("New keybinds are: ");
                //     Debug.Log("FF: " + GameData.flip_front_key + "\tFB: " + GameData.flip_back_key + "\tRL" +
                //               GameData.roll_left_key + "\tRR" + GameData.roll_right_key + "\tSL" +
                //               GameData.spin_left_key + "\tSR" + GameData.spin_right_key);
                // }
            // }
        }
    }

    void refresh_KB_Highlights()
    {
        // if (screenNum == 2)
        // {
        //     for (int i = 0; i < 62; i++)
        //     {
        //         KeyCode k = KeyCodes[i];
        //         // if(k == GameData.jump_key){
        //         // if (k == GameData.flip_front_key || k == GameData.flip_back_key || k == GameData.roll_left_key ||
        //         //     k == GameData.roll_right_key || k == GameData.spin_left_key || k == GameData.spin_right_key)
        //         // {
        //             ColorBlock colorBlock = kbButtons[i].GetComponent<Button>().colors;
        //             colorBlock.normalColor = Color.red;
        //             kbButtons[i].GetComponent<Button>().colors = colorBlock;
        //         }
        //
        //     }
        // }
        
        if (screenNum == 1)
        {
            for (int j = 0; j < 62; j++)
            {
                int i = j;

                // Debug.Log("Key: " + i + "\t" + KeyCodes2[i]);
                KeyCode k = KeyCodes2[i];
                // if (k == GameData.jump_key || k == GameData.move_left_key || k == GameData.move_right_key || k == GameData.look_back_key)
                // {
                
                ColorBlock colorBlock = kbButtons[i].GetComponent<Button>().colors;
                if (k == GameData.jump_key)
                {
                    colorBlock.normalColor = jmpColor;
                    kbButtons[i].GetComponent<Button>().colors = colorBlock;
                }
                else if (k == GameData.move_left_key)
                {
                    colorBlock.normalColor = leftColor;
                    kbButtons[i].GetComponent<Button>().colors = colorBlock;
                }
                else if (k == GameData.move_right_key)
                {
                    colorBlock.normalColor = rightColor;
                    kbButtons[i].GetComponent<Button>().colors = colorBlock;
                }
                else if (k == GameData.look_back_key)
                {
                    colorBlock.normalColor = lookBackColor;
                    kbButtons[i].GetComponent<Button>().colors = colorBlock;
                }
                else
                {
                    kbButtons[i].GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // if (needToUpdateKB)
        // {
        //     int kissmyass = 33;
        //     // int kissmyass = kbToUpdate;
        //     Debug.Log("NOW " + waiting + " " + GameData.jump_key + " KTU:" + kbToUpdate + " KMA:" + kissmyass);
        //     needToUpdateKB = false;
        //     ColorBlock colorBlock = kbButtons[kissmyass].GetComponent<Button>().colors;
        //     colorBlock.normalColor = jmpColor;
        //     kbButtons[kissmyass].GetComponent<Button>().colors = colorBlock;
        //     kbToUpdate = -1;
        //     // refresh_KB_Highlights();
        // }
    }
}
