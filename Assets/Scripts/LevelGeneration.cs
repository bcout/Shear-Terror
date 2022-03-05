using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private List<GameObject> all_blocks;
    private List<GameObject> available_blocks;

    [SerializeField]
    private Transform level_parent;

    [SerializeField]
    private GameObject starting_block;

    [SerializeField]
    private GameObject ending_block;

    void Start()
    {
        /*
        * GameData keeps an array of all the prefabs we want to use to generate levels.
        * The values are set in the inspector
        */
        all_blocks = new List<GameObject>(GetComponent<GameData>().all_level_blocks);
        GenerateLevel();
    }

    /*
     * Start with a block, spawn a random block after it. If their box colliders collide, remove that block
     * and spawn a different one. Repeat until there are 8 blocks end to end.
     */
    private bool GenerateLevel()
    {
        GameObject prev_block = starting_block;
        GameObject curr_block;
        Transform prev_end = prev_block.transform.Find("End");
        int index = 0;

        // Spawn the starting block at the world origin
        Instantiate(starting_block, Vector3.zero, Quaternion.identity, level_parent);

        // Spawn the middle blocks one after the other
        while (index < Constants.NUM_BLOCKS_IN_LEVEL)
        {
            // Grab a random block from GameData's list
            int block_index = Random.Range(0, all_blocks.Count - 1);
            curr_block = all_blocks[block_index];

            // Each block has an "end" object, giving us access to the exact position of the far end of the block
            prev_end = prev_block.transform.Find("End");

            // Make sure available_blocks matches all_blocks at the beginning of each iteration
            available_blocks = all_blocks;

            /*
             * This while loop is the smarts in this level generation algorithm
             * If the generation gets to a point where the block it just spawned will result in the next block colliding
             * with an existing block, remove the one that was just spawned and try a different one.
             * 
             * If there are no other blocks available (all of them cause collisions), then this level isn't going to work, so abandon it.
             */
            bool successful = false;
            while (!successful)
            {
                /*
                 * The pivot point of every block is not at the center of the object, but at the start point.
                 * This means we can just spawn the new block at the last block's end point and they line up perfectly.
                 */
                curr_block = Instantiate(curr_block, prev_end.position, prev_end.rotation, level_parent);

                /*
                 * Check if there is space for a block after placing the current one.
                 * We don't want the next block we place to collide with an existing object, and we need
                 * to ensure there is space for the last block (placed on its own after the level is generated),
                 * so,check to make sure there is space ahead of where we're about to place the block.
                 * 
                 * We instantiate before checking for space because we need the box collider's values for the calculation,
                 * and those values are only set when the prefab is instantiated.
                 * I'm sure this can be optimized, just not a priority now.
                 */
                if (!CheckForUpcomingCollision(curr_block))
                {
                    successful = true;
                }
                else
                {
                    // The block we just spawned will cause a collision, so remove it from both the scene and the list of available blocks
                    Destroy(curr_block);
                    available_blocks.RemoveAt(block_index);

                    // If there are no more blocks to choose from, abandon this level generation
                    if (available_blocks.Count == 0)
                    {
                        print("Abandoning level generation");
                        return false;
                    }

                    // Otherwise, pick a new block from the shortened list and try again
                    block_index = Random.Range(0, available_blocks.Count - 1);
                    curr_block = available_blocks[block_index];
                }
            }

            prev_block = curr_block;
            index++;
        }

        // Spawn the ending block
        prev_end = prev_block.transform.Find("End");
        Instantiate(ending_block, prev_end.position, prev_end.rotation, level_parent);

        return true;
    }

    private bool CheckForUpcomingCollision(GameObject curr_block)
    {
        // Get the center of the current block's box collider
        BoxCollider collider = curr_block.GetComponent<BoxCollider>();
        Vector3 curr_center = collider.bounds.center;
        print(curr_center);

        /*
         * The next block will be placed along one of the current box collider's 4 sides. The direction of
         * curr_end.forward tells us which side will be used.
         */
        Transform curr_end = curr_block.transform.Find("End");
        Vector3 next_center;
        if (curr_end.forward == Vector3.right)
        {
            // Next block will be to the right of the current block
            next_center = new Vector3(curr_center.x + Constants.MAX_BLOCK_SIZE, 0, curr_center.z);
        }
        else if (curr_end.forward == Vector3.left)
        {
            // Next block will be to the left
            next_center = new Vector3(curr_center.x - Constants.MAX_BLOCK_SIZE, 0, curr_center.z);
        }
        else if (curr_end.forward == Vector3.forward)
        {
            // Next block will be above the current block
            next_center = new Vector3(curr_center.x, 0, curr_center.z + Constants.MAX_BLOCK_SIZE);
        }
        else
        {
            // Next block will be below
            next_center = new Vector3(curr_center.x, 0, curr_center.z - Constants.MAX_BLOCK_SIZE);
        }

        Collider[] intersecting = Physics.OverlapSphere(next_center, 0.01f);
        return intersecting.Length != 0;
    }
}
