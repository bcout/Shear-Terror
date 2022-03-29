public static class PlayerData
{
    public enum Lane
    {
        LEFT,
        MIDDLE,
        RIGHT
    }

    public enum Trick
    {
        LEFT_SPIN,
        RIGHT_SPIN,
        FRONT_FLIP,
        BACK_FLIP,
        BARREL_ROLL_LEFT,
        BARREL_ROLL_RIGHT,
        NONE
    }

    public static Lane curr_lane { get; set; }
    public static Trick curr_trick { get; set; }
    public static bool on_ground { get; set; }

    static PlayerData()
    {
        // Start the player off in the middle lane
        curr_lane = Lane.MIDDLE;
        // Start the player off on the ground
        on_ground = true;
    }
}

