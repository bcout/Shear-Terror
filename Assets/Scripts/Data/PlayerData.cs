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
        SPIN,
        FLIP,
        NONE
    }

    public static Lane curr_lane { get; set; }
    public static Trick curr_trick { get; set; }

    static PlayerData()
    {
        // Start the player off in the middle lane
        curr_lane = Lane.MIDDLE;
    }
}

