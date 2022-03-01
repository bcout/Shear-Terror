using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecomputeLevels : MonoBehaviour
{
    private GameObject[] available_level_blocks;
    // Start is called before the first frame update
    void Start()
    {
        available_level_blocks = GetComponent<GameData>().available_level_blocks;
        
        /*
         * Start with a block, spawn a random block after it. If their box colliders collide, remove that block
         * and spawn a different one. Repeat until there are 8 blocks end to end.
         */
    }
}
