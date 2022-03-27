using UnityEngine;
public static class FarmerData
{
    public enum Lane
    {
        LEFT,
        MIDDLE,
        RIGHT
    }

    public static Lane curr_lane { get; set; }

    public static void SetCurrLane(GameObject lane)
    {
        switch (lane.name)
        {
            case Constants.LEFT_LANE_NAME:
                curr_lane = Lane.LEFT;
                break;
            case Constants.MIDDLE_LANE_NAME:
                curr_lane = Lane.MIDDLE;
                break;
            case Constants.RIGHT_LANE_NAME:
                curr_lane = Lane.RIGHT;
                break;
        }
    }
}