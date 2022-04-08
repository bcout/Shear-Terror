using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMenuController : MonoBehaviour
{

    public void GoToLevelLoadingScreen()
    {
        print("Going to loading screen...");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
