using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject sheep, farmer;

    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    private GameObject heartsCanvas;
    private GameObject gameOverCanvas;
        
    private LevelSpawner level_spawner;
    private SheepController sheep_controller;
    private FarmerController farmer_controller;
    private AsyncOperation op;

    private bool game_paused;

    // Start is called before the first frame update
    void Start()
    {
        heart1 = GameObject.FindGameObjectWithTag("Heart1");
        heart2 = GameObject.FindGameObjectWithTag("Heart2");
        heart3 = GameObject.FindGameObjectWithTag("Heart3");
        heartsCanvas = GameObject.FindGameObjectWithTag("HeartsCanvas");
        heartsCanvas.SetActive(true);
        gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas");
        gameOverCanvas.SetActive(false);
        LoadComponents();
        GenerateLevel();
        sheep_controller.StartLevel();
        farmer_controller.StartLevel();
    }

    private void LoadComponents()
    {
        level_spawner = GetComponent<LevelSpawner>();
        sheep_controller = sheep.GetComponent<SheepController>();
        farmer_controller = farmer.GetComponent<FarmerController>();
    }

    private void GenerateLevel()
    {
        level_spawner.GenerateLevel(GameData.level_pool_id);
    }

    public void restartBtn()
    {
        GameData.isGameOver = false;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainMenuBtn()
    {
        GameData.isGameOver = false;
        GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
        StartCoroutine(LoadSceneAsync("Title"));
    }

    // Call this whenever the sheep goes ragdoll.
    public void decrementLife()
    {
        GameData.sheepLivesRemaining--;
        if (GameData.sheepLivesRemaining == 2)
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
            heartsCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            GameData.isGameOver = true;
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
}
