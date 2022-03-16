using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LevelSpawner level_spawner;
    private SheepController sheep_controller;

    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
        GenerateLevel();
        sheep_controller.StartLevel();
    }

    private void LoadComponents()
    {
        level_spawner = GetComponent<LevelSpawner>();
        sheep_controller = GameObject.Find("Sheep").GetComponent<SheepController>();
    }

    private void GenerateLevel()
    {
        level_spawner.GenerateLevel(Constants.SHORT_LEVEL_ID);
    }
}
