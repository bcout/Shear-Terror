public static class GameData
{
    public enum State
    {
        PAUSED,
        RUNNING,
        LOADING
    }

    public static State state { get; set; }

    static GameData()
    {
        state = State.PAUSED;
    }
}
