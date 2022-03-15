using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
 * This class contains all the information about a single level.
 * It has a list of all the blocks used to make the level.
 */
public class LevelSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject short_straight, long_straight, left_turn, right_turn;

    private GameObject level_parent;
    private int[] IDs_to_spawn;
    private GameObject[] blocks_to_spawn;

    private GameObject prev_block, curr_block;
    private Transform prev_end;

    /*
     * The game controller calls this when it is time to start a level. This script
     * pulls a random line from one of the level data files (short_levels.csv or long_levels.csv) and 
     * spawns in an object according to each value in that line.
     * 
     * level_code: SHORT_LEVEL_ID or LONG_LEVEL_ID, tells the function what length of level should be generated
     */
    public void GenerateLevel(int level_code)
    {
        level_parent = new GameObject("Level Parent");
        IDs_to_spawn = PickRandomLevel(level_code);
        blocks_to_spawn = IDsToBlocks(IDs_to_spawn);

        prev_block = blocks_to_spawn[0];
        for (int i = 0; i < blocks_to_spawn.Length; i++)
        {
            curr_block = blocks_to_spawn[i];
            prev_end = prev_block.transform.Find("End");

            curr_block = Instantiate(curr_block, prev_end.position, prev_end.rotation, level_parent.transform);
            prev_block = curr_block;
        }
    }

    private int[] PickRandomLevel(int level_code)
    {
        int[] to_return;
        string filepath = (level_code == Constants.SHORT_LEVEL_ID) ? Application.streamingAssetsPath + Constants.SHORT_LEVEL_DATA_PATH : Application.dataPath + Constants.LONG_LEVEL_DATA_PATH;

        int num_available_levels = File.ReadLines(filepath).Count();

        // Pick a random line in the file
        System.Random rand = new System.Random(DateTime.Now.Second);
        int random_level = rand.Next(1, num_available_levels);
        string line = File.ReadLines(filepath).Skip(random_level - 1).Take(1).First();

        to_return = Array.ConvertAll(line.Split(','), int.Parse);

        return to_return;
    }

    /*
     * This function will be modified to pull a random terrain block from a list of pre-made blocks.
     * But for now, it just spawns the base building block
     */
    private GameObject GetBlock(int ID)
    {
        GameObject to_return = null;
        switch (ID)
        {
            case Constants.SHORT_STRAIGHT_ID:
                to_return = short_straight;
                break;
            case Constants.LONG_STRAIGHT_ID:
                to_return = long_straight;
                break;
            case Constants.LEFT_TURN_ID:
                to_return = left_turn;
                break;
            case Constants.RIGHT_TURN_ID:
                to_return = right_turn;
                break;
        }

        return to_return;
    }

    private GameObject[] IDsToBlocks(int[] IDs)
    {
        GameObject[] to_return = new GameObject[IDs.Length];
        for (int i = 0; i < IDs.Length; i++)
        {
            to_return[i] = GetBlock(IDs[i]);
        }

        return to_return;
    }
}
