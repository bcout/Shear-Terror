using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * This class contains all the information about a single level.
 * It has a list of all the blocks used to make the level.
 */
public class LevelSpawner : MonoBehaviour {


    private GameObject level_parent;
    private int[] blocks_to_spawn;

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
        blocks_to_spawn = PickRandomLevel(level_code);
    }

    private int[] PickRandomLevel(int level_code)
    {
        string filepath = (level_code == Constants.SHORT_LEVEL_ID) ? Application.streamingAssetsPath + GameData.SHORT_LEVEL_DATA_PATH : Application.dataPath + GameData.LONG_LEVEL_DATA_PATH;
        print(filepath);

        return null;
    }
}
