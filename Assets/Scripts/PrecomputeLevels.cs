using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecomputeLevels : MonoBehaviour
{
    private GameObject[] available_blocks;
    private GameObject[] level_blocks;
    private const int NUM_BLOCKS_IN_LEVEL = 8;

    [SerializeField]
    private Transform level_parent;
    // Start is called before the first frame update
    void Start()
    {
        available_blocks = GetComponent<GameData>().available_level_blocks;
        level_blocks = new GameObject[NUM_BLOCKS_IN_LEVEL];
        
        /*
         * Start with a block, spawn a random block after it. If their box colliders collide, remove that block
         * and spawn a different one. Repeat until there are 8 blocks end to end.
         */
        foreach (GameObject block in available_blocks)
        {
            print(block.name);
        }

        int index = Random.Range(0, available_blocks.Length - 1);
        GenerateLevel(available_blocks[index]);
    }

    private void GenerateLevel(GameObject starting_block)
    {
        GameObject prev_block = starting_block;
        GameObject curr_block;
        int index = 0;

        Instantiate(starting_block, Vector3.zero, Quaternion.identity, level_parent);

        while (index < NUM_BLOCKS_IN_LEVEL)
        {
            curr_block = available_blocks[Random.Range(0, available_blocks.Length)];
            Transform prev_end = prev_block.transform.Find("End");
            Transform curr_start = curr_block.transform.Find("Start");
            Vector3 spawn_point = prev_end.position + (curr_block.transform.position - curr_start.position);

            prev_block = Instantiate(curr_block, spawn_point, prev_end.rotation, level_parent);
            index++;
        }

    }
}
