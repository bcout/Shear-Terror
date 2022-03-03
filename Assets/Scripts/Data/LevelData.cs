using System.Collections.Generic;
using UnityEngine;

/*
 * This class contains all the information about a single level.
 * It has a list of all the blocks used to make the level.
 */
public class LevelData : MonoBehaviour { 
    /*
     * This is a list of all the blocks that make up the level, in order
     * blocks[0] is the first block, blocks[n-1] is the last
     * 
     * We use a list instead of an array because the level generation process involves adding blocks to this list,
     * which makes List<T> more efficient
     */
    private List<Transform> blocks;

    /*
     * This function generates a sequence of blocks to form a level, drawing from GameData.available_blocks to do so.
     * None of the blocks intersect, and each level is noticeably different from the one before it
     * 
     * The player uses this list to move through the level.
     * 
     * It would be best to pre-compute all the valid permutations of the blocks, then this function just reads one of them from a file
     * and loads it into the array.
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
