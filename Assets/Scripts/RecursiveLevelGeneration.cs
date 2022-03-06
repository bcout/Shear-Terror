using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveLevelGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject starting_block;

    void Start()
    {
        //GenerateLevel();
        
        List<GameObject> blocks = FindValidBlocks(starting_block);
        foreach (GameObject block in blocks)
        {
            print(block.name);
        }
    }

    private void GenerateLevel()
    {
        // setup

        // Call the recursive function
        GenerateLevelRecursive(starting_block, 0);
    }

    private bool GenerateLevelRecursive(GameObject curr_block, int num_blocks_placed)
    {
        /*
         * Base case 1:
         * We have successfully placed all n blocks
         */
        if (num_blocks_placed == Constants.NUM_BLOCKS_IN_LEVEL)
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
         * If we get here it means we haven't placed all the blocks yet, and we are not in a dead end
         * Use the magic of recursion to check every block
         */

        // TEMP
        GameObject next_block = null;
        bool successful = GenerateLevelRecursive(next_block, num_blocks_placed++);
        

        // TEMP
        return true;
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
        GameObject[] available_blocks = GetComponent<GameData>().all_level_blocks;
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
        next_block = Instantiate(next_block, curr_block_end.position, curr_block_end.rotation);

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

        Destroy(next_block);

        return intersecting.Length != 0;
    }
}
