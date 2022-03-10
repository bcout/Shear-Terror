using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Start by exporting the short levels to their own file
        foreach (Transform level in short_level_parent.transform)
        {
            level_blocks = new GameObject[level.childCount];
            for(int i = 0; i < level_blocks.Length; i++)
            {
                level_blocks[i] = level.GetChild(i).gameObject;
            }

            level_blocks_mapped = MapLevelBlocks(level_blocks);
            foreach(int value in level_blocks_mapped)
            {
                print(value);
            }
        }

        /*
        foreach (Transform level in long_level_parent.transform)
        {
            print(level.childCount);
        }
        */
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
                    mapped_value = 0;
                    break;
                case LONG_STRAIGHT:
                    mapped_value = 1;
                    break;
                case LEFT_TURN:
                    mapped_value = 2;
                    break;
                case RIGHT_TURN:
                    mapped_value = 3;
                    break;
            }
            to_return[i] = mapped_value;
        }

        return to_return;
    }
}
