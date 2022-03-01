using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour { 
    /*
     * This is a list of all the blocks that make up the level, in order
     * blocks[0] is the first block, blocks[n-1] is the last
     */
    private List<Transform> blocks;
    private GameObject[] available_blocks;

    private void Start()
    {
        available_blocks = Resources.LoadAll("Level Blocks") as GameObject[];
    }

    /*
     * This function generates a sequence of blocks to form a level, drawing from available_blocks to do so.
     * None of the blocks intersect, and each level is noticeably different from the one before it
     * 
     * The player uses this list to move through the level
     */
    public List<Transform> GenerateLevel()
    {
        // TODO: Implement this function
        return blocks;
    }

    /*
     * Returns the blocks that make up the level this script is attached to.
     * This is the same list that was generated at the beginning, when GenerateLevel() was called
     */
    public List<Transform> GetLevelBlocks()
    {
        return blocks;
    }

    public void RemoveBlock(int index)
    {
        //Opportunity to put some checks in here before the block is removed
        blocks.RemoveAt(index);
    }

    public void AddBlock(Transform block)
    {
        //Opportunity to put some checks in here before the block is added
        blocks.Add(block);
    }
}
