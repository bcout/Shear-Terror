using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    public GameObject panel;
    public Text nextButtonText;
    private AsyncOperation op;
    
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.current_level >= 2)
        {
            nextButtonText.text = "Continue";
        }
        else
        {
            nextButtonText.text = "Next Level";
        }
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.won_level)
        {
            panel.SetActive(true);
            score.text = "Score: " + GameData.score;
            GameData.won_level = false;
        }

        //Used in testing to auto win levels.
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameData.won_level_for_movement = true;
            GameData.won_level = true;
        }
    }
    // Load the next level from the win screen.
    public void LoadNextLevel()
    {
        GameData.won_level_for_movement = false;
        GameData.score = 0;
        GameData.firstTrick = true;
        GameData.current_level++;
        if (GameData.current_level < 3)
        {
            GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
            StartCoroutine(LoadSceneAsync("Loading"));
        }
        else if (GameData.current_level == 3)
        {
            GameData.current_level = 1;
            GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
            StartCoroutine(LoadSceneAsync("Outro"));
        }
    }
    
    IEnumerator LoadSceneAsync (string level)
    {
        op = SceneManager.LoadSceneAsync(level);
        while (!op.isDone)
        {
            yield return null;
        }
    }
    
    public void QuitToMainMenu()
    {
        GameData.score = 0;
        GameData.firstTrick = true;
        GameData.current_level = 1;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
