public static class PlayerData
{
    public enum Lanes
    {
        LEFT,
        MIDDLE,
        RIGHT
    }

    public static Lanes curr_lane { get; set; }

    static PlayerData()
    {
        // Start the player off in the middle lane
        curr_lane = Lanes.MIDDLE;
    }
}

