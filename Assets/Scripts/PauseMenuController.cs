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
    }

    public void Pause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
