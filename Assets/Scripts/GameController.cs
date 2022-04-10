using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject sheep, farmer;
    public LevelScreenController lvl_screen;
    
    private LevelSpawner level_spawner;
    private SheepController sheep_controller;
    private FarmerController farmer_controller;

    private bool game_paused;

    private void Awake()
    {
        LoadLocalComponents();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
        GenerateLevel();
        sheep_controller.StartLevel();
        farmer_controller.StartLevel();
        GameData.firstTrick = true;
    }

    private void LoadLocalComponents()
    {
        level_spawner = GetComponent<LevelSpawner>();
    }

    private void LoadComponents()
    {
        sheep_controller = sheep.GetComponent<SheepController>();
        farmer_controller = farmer.GetComponent<FarmerController>();
    }

    private void GenerateLevel()
    {
        level_spawner.GenerateLevel(GameData.level_pool_id);
    }

    // Call this whenever the sheep goes ragdoll.
    public void decrementLife()
    {
        GameData.sheepLivesRemaining--;
        lvl_screen.updateHearts();
    }
}
