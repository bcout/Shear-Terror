using UnityEngine;

public class BlockData : MonoBehaviour
{
    [SerializeField]
    private Transform left_lane, middle_lane, right_lane;

    public Transform GetLane(PlayerData.Lane lane)
    {
        Transform to_return = null;
        switch (lane)
        {
            case PlayerData.Lane.LEFT:
                to_return = left_lane;
                break;
            case PlayerData.Lane.MIDDLE:
                to_return = middle_lane;
                break;
            case PlayerData.Lane.RIGHT:
                to_return = right_lane;
                break;
        }

        return to_return;
    }
}


