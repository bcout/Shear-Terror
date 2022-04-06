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

    [SerializeField] private GameObject[] short_straights, long_straights, left_turns, right_turns;
    [SerializeField] private GameObject[] short_straight_obstacle_sets, long_straight_obstacle_sets, left_turn_obstacle_sets, right_turn_obstacle_sets;

    private GameObject level_parent, obstacle_parent;
    private int[] IDs_to_spawn;
    private GameObject[] blocks_to_spawn;
    private GameObject[] obstacles_to_spawn;

    private GameObject prev_block, curr_block;
    private Transform prev_end;

    private System.Random rand;

    /*
     * The game controller calls this when it is time to start a level. This script
     * pulls a random line from one of the level data files (short_levels.csv or long_levels.csv) and 
     * spawns in an object according to each value in that line.
     * 
     * level_code: SHORT_LEVEL_ID or LONG_LEVEL_ID, tells the function what length of level should be generated
     */
    public void GenerateLevel(int level_code)
    {
        rand = new System.Random(DateTime.Now.Second);
        level_parent = new GameObject(Constants.LEVEL_PARENT_NAME);
        obstacle_parent = new GameObject(Constants.OBSTACLE_PARENT_NAME);
        IDs_to_spawn = PickRandomLevel(level_code);
        blocks_to_spawn = IDsToBlocks(IDs_to_spawn);
        obstacles_to_spawn = PickObstacleSets(IDs_to_spawn);

        prev_block = blocks_to_spawn[0];
        for (int i = 0; i < blocks_to_spawn.Length; i++)
        {
            curr_block = blocks_to_spawn[i];
            prev_end = prev_block.transform.Find("End");

            curr_block = Instantiate(curr_block, prev_end.position, prev_end.rotation, level_parent.transform);

            if (i != 0 && obstacles_to_spawn[i] != null)
            {
                Instantiate(obstacles_to_spawn[i], curr_block.transform.Find("Start").position, curr_block.transform.rotation, obstacle_parent.transform);
            }

            prev_block = curr_block;
        }
    }

    private int[] PickRandomLevel(int level_code)
    {
        int[] to_return;
        string filepath = (level_code == Constants.SHORT_LEVEL_ID) ? Application.streamingAssetsPath + Constants.SHORT_LEVEL_DATA_PATH : Application.dataPath + Constants.LONG_LEVEL_DATA_PATH;

        int num_available_levels = File.ReadLines(filepath).Count();

        // Pick a random line in the file
        int random_level = rand.Next(1, num_available_levels);
        string line = File.ReadLines(filepath).Skip(random_level - 1).Take(1).First();

        to_return = Array.ConvertAll(line.Split(','), int.Parse);

        return to_return;
    }

    private GameObject GetBlock(int ID)
    {
        GameObject to_return;
        GameObject[] block_list;
        int random_index;

        switch (ID)
        {
            case Constants.SHORT_STRAIGHT_ID:
                block_list = short_straights;
                break;
            case Constants.LONG_STRAIGHT_ID:
                block_list = long_straights;
                break;
            case Constants.LEFT_TURN_ID:
                block_list = left_turns;
                break;
            case Constants.RIGHT_TURN_ID:
            default:
                block_list = right_turns;
                break;
        }
        
        random_index = rand.Next(0, block_list.Length);
        to_return = block_list[random_index];
        
        return to_return;
    }

    private GameObject GetRandomObstacleSet(int ID)
    {
        GameObject to_return;
        GameObject[] obstacle_list;
        int random_index;

        switch (ID)
        {
            case Constants.SHORT_STRAIGHT_ID:
                obstacle_list = short_straight_obstacle_sets;
                break;
            case Constants.LONG_STRAIGHT_ID:
                obstacle_list = long_straight_obstacle_sets;
                break;
            case Constants.LEFT_TURN_ID:
                obstacle_list = left_turn_obstacle_sets;
                break;
            case Constants.RIGHT_TURN_ID:
            default:
                obstacle_list = right_turn_obstacle_sets;
                break;
        }

        if (obstacle_list.Length == 0)
        {
            return null;
        }

        random_index = rand.Next(0, obstacle_list.Length);
        to_return = obstacle_list[random_index];

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

    private GameObject[] PickObstacleSets(int[] IDs)
    {
        GameObject[] to_return = new GameObject[IDs.Length];
        for (int i = 0; i < IDs.Length; i++)
        {
            to_return[i] = GetRandomObstacleSet(IDs[i]);
        }

        return to_return;
    }
}
