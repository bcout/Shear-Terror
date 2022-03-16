public static class GameData
{
    public static bool game_started { get; set; }
    static GameData()
    {
        game_started = false;
    }
}
