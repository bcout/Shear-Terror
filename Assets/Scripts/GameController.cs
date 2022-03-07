using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject player_object;

    private LevelData level;
    // Start is called before the first frame update
    void Start()
    {
        level = GetComponent<LevelData>();
        level.GenerateLevel();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(player_object);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
