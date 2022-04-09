using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class WinScreenController : MonoBehaviour
{
    public GameObject panel;
    private AsyncOperation op;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.won_level)
        {
            GameData.won_level = false;
            panel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameData.won_level = true;
        }
    }

    public void LoadNextLevel()
    {
        GameData.current_level++;
        if (GameData.current_level < 3)
        {
            GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
            StartCoroutine(LoadSceneAsync("Loading"));
        }
        else if (GameData.current_level == 3)
        {
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
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
