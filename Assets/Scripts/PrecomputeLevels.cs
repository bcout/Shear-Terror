using UnityEngine;

public class PrecomputeLevels : MonoBehaviour
{
    private GameObject[] available_blocks;
    private GameObject[] level_blocks;
    private const int NUM_BLOCKS_IN_LEVEL = 8;

    [SerializeField]
    private Transform level_parent;

    void Start()
    {
        /*
         * GameData keeps an array of all the prefabs we want to use to generate levels.
         * The values are set in the inspector
         */
        available_blocks = GetComponent<GameData>().available_level_blocks;
        level_blocks = new GameObject[NUM_BLOCKS_IN_LEVEL];
        
        
        foreach (GameObject block in available_blocks)
        {
            print(block.name);
        }

        int index = Random.Range(0, available_blocks.Length - 1);
        GenerateLevel(available_blocks[index]);
    }

    /*
     * Start with a block, spawn a random block after it. If their box colliders collide, remove that block
     * and spawn a different one. Repeat until there are 8 blocks end to end.
     */
    private void GenerateLevel(GameObject starting_block)
    {
        GameObject prev_block = starting_block;
        GameObject curr_block;
        int index = 0;

        // Spawn the starting block at the world origin
        Instantiate(starting_block, Vector3.zero, Quaternion.identity, level_parent);

        while (index < NUM_BLOCKS_IN_LEVEL)
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

            // Check if the box colliders intersect
            BoxCollider collider = curr_block.GetComponent<BoxCollider>();
            

            index++;
        }

    }
}
