using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenController : MonoBehaviour
{
    public GameObject level_screen_buttons;
    public GameObject level_screen_sign;
    public GameObject panel;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    // Start is called before the first frame update
    void Start()
    {
        // level_screen_panel = GameObject.FindGameObjectWithTag("GameOverPanel");
        // heart1 = GameObject.FindGameObjectWithTag("Heart1");
        // heart2 = GameObject.FindGameObjectWithTag("Heart2");
        // heart3 = GameObject.FindGameObjectWithTag("Heart3");
        // heart3 = GameObject.FindGameObjectWithTag("Heart4");
        // heart3 = GameObject.FindGameObjectWithTag("Heart5");
        level_screen_buttons.SetActive(false);
        level_screen_sign.SetActive(false);
    }

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
            // heartsCanvas.SetActive(false);
            // gameOverCanvas.SetActive(true);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitToMainMenu()
    {
        GameData.isGameOver = false;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }

    public void hidePanel()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
