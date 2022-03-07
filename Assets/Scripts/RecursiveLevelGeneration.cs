using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RecursiveLevelGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject starting_block;

    private GameObject[] available_blocks;
    private GameObject block_pool_parent;

    private List<GameObject> blocks_to_spawn;
    private System.Random rand;

    void Start()
    {
        CreateObjectPool();

        rand = new System.Random(DateTime.Now.Second);
        blocks_to_spawn = new List<GameObject>();

        GenerateLevel();

        blocks_to_spawn.Reverse();

        for(int i = 0; i < blocks_to_spawn.Count; i++)
        {
            print(blocks_to_spawn[i].name);
        }

        EmptyObjectPool();
        
    }

    private void GenerateLevel()
    {
        // setup

        // Call the recursive function
        GenerateLevelRecursive(starting_block, starting_block, 0);
    }

    /*
     * Unfinished, need to figure out the actual recursive call part and instantiating blocks in the right order
     */
    private bool GenerateLevelRecursive(GameObject curr_block, GameObject prev_block, int num_blocks_placed)
    {
        /*
         * Base case 1:
         * We have successfully placed all n blocks
         */
        if (num_blocks_placed >= Constants.NUM_BLOCKS_IN_LEVEL)
        {
            return true;
        }

        /*
         * Base case 2:
         * We have tried every possible block and they all result in collisions
         */
        List<GameObject> valid_blocks = FindValidBlocks(curr_block);
        if (valid_blocks.Count == 0)
        {
            return false;
        }

        /*
         * Spawn the block
         */
        int index = rand.Next(0, valid_blocks.Count);
        GameObject next_block = valid_blocks[index];

        num_blocks_placed++;
        return GenerateLevelRecursive(next_block, curr_block, num_blocks_placed);
    }

    /*
     * Goes through each block and attempts to place it after the current block. If it will result in a collision,
     * move to the next block
     * If one of the blocks works, place it in a list.
     * Repeat for all blocks
     * If none of the available blocks are valid, returns an empty list
     */
    private List<GameObject> FindValidBlocks(GameObject curr_block)
    {
        
        List<GameObject> valid_blocks = new List<GameObject>();
        
        for (int i = 0; i < available_blocks.Length; i++)
        {
            if (!CheckForUpcomingCollision(curr_block, available_blocks[i]))
            {
                // The block at i is valid
                valid_blocks.Add(available_blocks[i]);
            }
        }

        return valid_blocks;
    }

    /*
     * This function determines if the following statement is true:
     * "If we spawned the given block, is there space for another block after it?"
     * If no, return false. Else, return true
     * 
     * We assume each block takes up 200x200, since that is the size of the largest block
     */
    private bool CheckForUpcomingCollision(GameObject curr_block, GameObject next_block)
    {
        // Place the next block after the current block and check its collision
        Transform curr_block_end = curr_block.transform.Find("End");
        next_block.transform.position = curr_block_end.position;
        next_block.transform.rotation = curr_block_end.rotation;

        Vector3 next_block_center = next_block.transform.Find("Center").position;
        Transform next_block_end = next_block.transform.Find("End");
        Vector3 next_next_center;

        if (next_block_end.forward == Vector3.right)
        {
            // Next block will be to the right of the current block
            next_next_center = new Vector3(next_block_center.x + Constants.MAX_BLOCK_SIZE, 0, next_block_center.z);
        }
        else if (next_block_end.forward == Vector3.left)
        {
            // Next block will be to the left
            next_next_center = new Vector3(next_block_center.x - Constants.MAX_BLOCK_SIZE, 0, next_block_center.z);
        }
        else if (next_block_end.forward == Vector3.forward)
        {
            // Next block will be above the current block
            next_next_center = new Vector3(next_block_center.x, 0, next_block_center.z + Constants.MAX_BLOCK_SIZE);
        }
        else
        {
            // Next block will be below
            next_next_center = new Vector3(next_block_center.x, 0, next_block_center.z - Constants.MAX_BLOCK_SIZE);
        }

        Collider[] intersecting = Physics.OverlapSphere(next_next_center, 0.01f);

        return intersecting.Length != 0;
    }

    /*
     * Instead of having CheckForUpcomingCollision() instantiate a block each time to check its collision,
     * instantiate all the available ones here and just have CheckForUpcomingCollision() position the instance it needs
     * into place.
     */
    private void CreateObjectPool()
    {
        available_blocks = GetComponent<LevelData>().all_level_blocks;
        block_pool_parent = new GameObject("Block Pool");
        for (int i = 0; i < available_blocks.Length; i++)
        {
            available_blocks[i] = Instantiate(available_blocks[i], block_pool_parent.transform);
            available_blocks[i].SetActive(false);
        }
    }

    private void EmptyObjectPool()
    {
        foreach (GameObject block in available_blocks)
        {
            Destroy(block);
        }
        Destroy(block_pool_parent);
    }
}
