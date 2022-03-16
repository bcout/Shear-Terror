using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject player_object;

    private LevelSpawner level_spawner;
    // Start is called before the first frame update
    void Start()
    {
        level_spawner = GetComponent<LevelSpawner>();
        level_spawner.GenerateLevel(Constants.SHORT_LEVEL_ID);
        GameData.game_started = true;
    }

    private void SpawnPlayer()
    {
        //Instantiate(player_object);
    }
}
