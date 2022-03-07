using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    [SerializeField]
    private Transform left_lane, middle_lane, right_lane;

    public Transform GetLane(PlayerData.Lanes lane)
    {
        Transform to_return = null;
        switch (lane)
        {
            case PlayerData.Lanes.LEFT:
                to_return = left_lane;
                break;
            case PlayerData.Lanes.MIDDLE:
                to_return = middle_lane;
                break;
            case PlayerData.Lanes.RIGHT:
                to_return = right_lane;
                break;
        }

        return to_return;
    }
}


