using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScreenController : MonoBehaviour
{
    public GameObject level_screen_buttons;
    public GameObject level_screen_sign;
    public Text score;
    public GameObject scorePanel;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    // Start is called before the first frame update
    void Start()
    {
        scorePanel.SetActive(true);
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
        GameData.score = 0;
        GameData.firstTrick = true;
        GameData.isGameOver = false;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene("Loading");
    }
    
    public void QuitToMainMenu()
    {
        GameData.score = 0;
        GameData.firstTrick = true;
        GameData.current_level = 1;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        GameData.isGameOver = false;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.won_level_for_movement)
        {
            GameData.won_level_for_movement = false;
            scorePanel.SetActive(false);
        }
        else
        {
            score.text = "Score: " + GameData.score;
        }
    }
}
