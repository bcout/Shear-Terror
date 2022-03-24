using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneChangeTrigger : MonoBehaviour
{
    // false: moved left, true: moved right
    private bool direction;

    public bool GetDirection()
    {
        return direction;
    }

    public void SetDirection(bool value)
    {
        direction = value;
    }
}
