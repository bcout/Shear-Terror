using UnityEngine;

public static class Constants
{
    public const float CAMERA_X_ROTATION = 18.23f;

    public const float RAGDOLL_FORCE = 1000f;
    public const float MIN_ROTATION_TO_LAND = 0.75f;

    public const float BASE_MOVEMENT_SPEED = 0.3f;
    public const float ROTATION_CHANGE_IN_TURNS = 90f;

    public const float JUMP_HEIGHT = 10f;
    public const float JUMP_SPEED = 2.5f;

    public const float SPIN_SPEED = 2f;
    public const float SPIN_ROTATION = 360f;

    public const int SHORT_LEVEL_ID = 1;
    public const int LONG_LEVEL_ID = 2;

    public const int SHORT_STRAIGHT_ID = 0;
    public const int LONG_STRAIGHT_ID = 1;
    public const int LEFT_TURN_ID = 2;
    public const int RIGHT_TURN_ID = 3;

    public const string SHORT_STRAIGHT_TAG = "Short Straight";
    public const string LONG_STRAIGHT_TAG = "Long Straight";
    public const string LEFT_TURN_TAG = "Left Turn";
    public const string RIGHT_TURN_TAG = "Right Turn";

    public const string LANE_END_NAME = "End";
    public const string LANE_START_NAME = "Start";
    public const string BLOCK_CENTER_NAME = "Center";

    public const string LEFT_LANE_NAME = "Left";
    public const string MIDDLE_LANE_NAME = "Middle";
    public const string RIGHT_LANE_NAME = "Right";

    public const string SHORT_LEVEL_DATA_PATH = "/LevelData/short_levels.csv";
    public const string LONG_LEVEL_DATA_PATH = "/LevelData/long_levels.csv";

    public const string LEVEL_PARENT_NAME = "Level Parent";
    public const string OBSTACLE_PARENT_NAME = "Obstacle Parent";

    public const string IDLE_ANIM = "Idle";
    public const string RUN_ANIM = "Run";
    public const string JUMP_UP_ANIM = "Jump Up";
    public const string FALL_IDLE_ANIM = "Fall Idle";
    public const string LANDING_ANIM = "Landing";
    public const string TUCK_IN_ANIM = "Tuck In";
    public const string TUCK_OUT_ANIM = "Tuck Out";

    public const string FARM_RUN_ANIM = "Run";
    public const string FARM_WAIT_ANIM = "Wait";
    public const string FARM_END_ANIM = "End";

    public const string LANE_CHANGE_TRIGGER_TAG = "Lane Change Trigger";

    public const string MAIN_MENU_SCENE_NAME = "Title";

    public const string PIVOT = "Pivot";
    public const string ARMATURE = "Pivot/Model/Armature";
}
