using UnityEngine;

public static class PlayerData
{
    public enum Lanes
    {
        LEFT,
        MIDDLE,
        RIGHT
    }

    public enum State
    {
        IDLE,
        RUNNING,
        RAGDOllING,
        JUMPING
    }

    public static Lanes curr_lane { get; set; }
    public static State state { get; set; }

    static PlayerData()
    {
        // Start the player off in the middle lane
        curr_lane = Lanes.MIDDLE;
        state = State.IDLE;
    }
}

