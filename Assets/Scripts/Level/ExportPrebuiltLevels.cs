using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ExportPrebuiltLevels : MonoBehaviour
{
    // Short level parent has 45-block levels as children. Long level parent has 60-block levels as children.
    [SerializeField]
    private GameObject short_level_parent, long_level_parent;

    private GameObject[] level_blocks;
    private int[] level_blocks_mapped;

    private const string SHORT_STRAIGHT = "Short Straight(Clone)";
    private const string LONG_STRAIGHT = "Long Straight(Clone)";
    private const string LEFT_TURN = "LeftTurn(Clone)";
    private const string RIGHT_TURN = "Right Turn(Clone)";

    // Start is called before the first frame update
    void Start()
    {
        WriteLevelData(Constants.SHORT_LEVEL_DATA_PATH, short_level_parent);
        WriteLevelData(Constants.LONG_LEVEL_DATA_PATH, long_level_parent);
    }

    private void WriteLevelData(string path, GameObject level_parent)
    {
        string full_path = Application.streamingAssetsPath + path;
        if (!File.Exists(full_path))
        {
            File.WriteAllText(full_path, "");
        }

        string output = "";
        // Start by exporting the short levels to their own file
        foreach (Transform level in level_parent.transform)
        {
            level_blocks = new GameObject[level.childCount];
            for (int i = 0; i < level_blocks.Length; i++)
            {
                level_blocks[i] = level.GetChild(i).gameObject;
            }

            level_blocks_mapped = MapLevelBlocks(level_blocks);

            // Write the normal level data
            output += WriteLevelData();

            // Reverse the level data and write it as a new level
            Array.Reverse(level_blocks_mapped);
            output += WriteLevelData();
        }

        File.WriteAllText(full_path, output);
    }

    private int[] MapLevelBlocks(GameObject[] blocks)
    {
        int[] to_return = new int[blocks.Length];
        for(int i = 0; i < blocks.Length; i++)
        {
            int mapped_value = -1;
            switch (blocks[i].name)
            {
                case SHORT_STRAIGHT:
                    mapped_value = Constants.SHORT_STRAIGHT_ID;
                    break;
                case LONG_STRAIGHT:
                    mapped_value = Constants.LONG_STRAIGHT_ID;
                    break;
                case LEFT_TURN:
                    mapped_value = Constants.LEFT_TURN_ID;
                    break;
                case RIGHT_TURN:
                    mapped_value = Constants.RIGHT_TURN_ID;
                    break;
            }
            to_return[i] = mapped_value;
        }

        return to_return;
    }

    private string WriteLevelData()
    {
        string output = "";
        for (int i = 0; i < level_blocks_mapped.Length - 1; i++)
        {
            output += level_blocks_mapped[i] + ",";
        }
        output += level_blocks_mapped[level_blocks_mapped.Length - 1];
        output += "\n";

        return output;
    }
}
