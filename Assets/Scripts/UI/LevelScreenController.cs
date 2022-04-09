using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenController : MonoBehaviour
{
    public GameObject level_screen_buttons;
    public GameObject level_screen_sign;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    // Start is called before the first frame update
    void Start()
    {
        level_screen_buttons.SetActive(false);
        level_screen_sign.SetActive(false);
    }

    // Update the lives indicator
    public void updateHearts()
    {
        if (GameData.sheepLivesRemaining == 4)
        {
            heart5.SetActive(false);
        }
        else if (GameData.sheepLivesRemaining == 3)
        {
            heart4.SetActive(false);
        }
        else if (GameData.sheepLivesRemaining == 2)
        {
            heart3.SetActive(false);
        }
        else if (GameData.sheepLivesRemaining == 1)
        {
            heart2.SetActive(false);
        }
        else if (GameData.sheepLivesRemaining == 0)
        {
            heart1.SetActive(false);
            level_screen_buttons.SetActive(false);
            level_screen_sign.SetActive(false);
            level_screen_buttons.SetActive(true);
            level_screen_sign.SetActive(true);
            GameData.isGameOver = true;
        }
    }
    
    public void RestartBtn()
    {
        GameData.isGameOver = false;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene("Loading");
    }
    
    public void QuitToMainMenu()
    {
        GameData.current_level = 1;
        GameData.isGameOver = false;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
