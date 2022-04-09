using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;

    private void Start()
    {
        pause_menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause_menu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1f;
        GameData.game_paused = false;
    }

    public void Pause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
        GameData.game_paused = true;
    }

    public void QuitToMainMenu()
    {
        GameData.score = 0;
        GameData.deaths = 0;
        GameData.current_level = 1;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        Time.timeScale = 1f;
        GameData.game_paused = false;
        GameData.currently_going_to_main_menu = true;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
