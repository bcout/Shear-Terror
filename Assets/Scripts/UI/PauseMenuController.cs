using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;
    // [SerializeField] GameObject frostedGlass;

    private void Start()
    {
        // frostedGlass.SetActive(false);
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
        // frostedGlass.SetActive(false);
        pause_menu.SetActive(false);
        Time.timeScale = 1f;
        GameData.game_paused = false;
    }

    public void Pause()
    {
        // frostedGlass.SetActive(true);
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
        GameData.game_paused = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        GameData.game_paused = false;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
