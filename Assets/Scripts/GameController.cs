using System.Collections;
using System.Collections.Generic;
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

        SpawnPlayer(level.GetBlocksInLevel()[0]);
    }

    private void SpawnPlayer(GameObject starting_block)
    {
        Vector3 start_pos = starting_block.transform.Find("Start").position;
        Quaternion start_orientation = starting_block.transform.Find("Start").rotation;
        GameObject player = Instantiate(player_object);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
