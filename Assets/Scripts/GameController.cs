using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject sheep, farmer;
    private LevelSpawner level_spawner;
    private SheepController sheep_controller;
    private FarmerController farmer_controller;

    private bool game_paused;

    // Start is called before the first frame update
    void Start()
    {
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
}
