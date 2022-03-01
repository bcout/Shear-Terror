using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    /**
     * 
     */
    private List<Transform> blocks;
    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<Transform>();
    }

    public List<Transform> GetBlocks()
    {
        return blocks;
    }

    public void RemoveBlock(int index)
    {
        blocks.RemoveAt(index);
    }

    public void AddBlock(Transform block)
    {
        blocks.Add(block);
    }
}
