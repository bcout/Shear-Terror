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
        int index = 0;

        // Spawn the starting block at the world origin
        curr_block = Instantiate(starting_block, Vector3.zero, Quaternion.identity, level_parent);

        while (index < Constants.NUM_BLOCKS_IN_LEVEL)
        {
            // Grab a random block from GameData's list
            curr_block = available_blocks[Random.Range(0, available_blocks.Length)];

            // Each block has an "end" object, giving us access to the exact position of the far end of the block
            Transform prev_end = prev_block.transform.Find("End");

            /*
             * The pivot point of every block is not at the center of the object, but at the start point.
             * This means we can just spawn the new block at the last block's location and they line up perfectly.
             */
            prev_block = Instantiate(curr_block, prev_end.position, prev_end.rotation, level_parent);

            // Reset prev_end since we've changed prev_block
            prev_end = prev_block.transform.Find("End");

            /*
             * Check if there is space for a block after placing the current one.
             * We don't want to place a block with the end pressing up against the side of an existing block.
             * So, check to make sure there is space ahead of where we're placing the block.
             */
            Vector3 center = prev_end.position;
            Quaternion orientation = prev_end.rotation;
            if (prev_end.forward == Vector3.right)
            {
                print("right");
            }
            else if (prev_end.forward == Vector3.left)
            {
                print("left");
            }
            else if (prev_end.forward == Vector3.forward)
            {
                print("up");
            }
            else if (prev_end.forward == Vector3.back)
            {
                print("down");
            }

            Instantiate(box_collider_object, center, orientation, level_parent);

            index++;
        }

    }
}
