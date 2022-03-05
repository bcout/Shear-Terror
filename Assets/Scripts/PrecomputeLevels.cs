using UnityEngine;

public class PrecomputeLevels : MonoBehaviour
{
    private GameObject[] available_blocks;
    private GameObject[] level_blocks;

    [SerializeField]
    private Transform level_parent;

    [SerializeField]
    private GameObject box_collider_object;

    [SerializeField]
    private GameObject starting_block;

    void Start()
    {
        /*
         * GameData keeps an array of all the prefabs we want to use to generate levels.
         * The values are set in the inspector
         */
        available_blocks = GetComponent<GameData>().available_level_blocks;
        level_blocks = new GameObject[Constants.NUM_BLOCKS_IN_LEVEL];

        int index = Random.Range(0, available_blocks.Length - 1);
        //GenerateLevel(available_blocks[index]);
        GenerateLevel();
    }

    /*
     * Start with a block, spawn a random block after it. If their box colliders collide, remove that block
     * and spawn a different one. Repeat until there are 8 blocks end to end.
     */
    private void GenerateLevel()
    {
        GameObject prev_block = starting_block;
        GameObject curr_block;
        Transform prev_end;
        int index = 0;

        // Spawn the starting block at the world origin
        Instantiate(starting_block, Vector3.zero, Quaternion.identity, level_parent);

        while (index < Constants.NUM_BLOCKS_IN_LEVEL)
        {
            // Grab a random block from GameData's list
            curr_block = available_blocks[Random.Range(0, available_blocks.Length)];

            // Each block has an "end" object, giving us access to the exact position of the far end of the block
            prev_end = prev_block.transform.Find("End");

            /*
             * The pivot point of every block is not at the center of the object, but at the start point.
             * This means we can just spawn the new block at the last block's end point and they line up perfectly.
             */
            curr_block = Instantiate(curr_block, prev_end.position, prev_end.rotation, level_parent);

            /*
             * Check if there is space for a block after placing the current one.
             * We don't want to place a block with the end pressing up against the side of an existing block.
             * So, check to make sure there is space ahead of where we're about to place the block.
             * 
             * We instantiate before checking for space because we need the box collider's values for the calculation,
             * and those values are only set when the prefab is instantiated.
             * I'm sure this can be optimized, just not a priority now.
             */
            if (CheckNextSpace(curr_block))
            {
                print("Colliding");
                break;
            }

            prev_block = curr_block;
            index++;
        }

    }

    private bool CheckNextSpace(GameObject curr_block)
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
