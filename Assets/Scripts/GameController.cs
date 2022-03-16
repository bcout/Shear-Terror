using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LevelSpawner level_spawner;
    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
        GenerateLevel();
    }

    private void LoadComponents()
    {
        level_spawner = GetComponent<LevelSpawner>();
    }

    private void GenerateLevel()
    {
        level_spawner.GenerateLevel(Constants.SHORT_LEVEL_ID);
    }
}
