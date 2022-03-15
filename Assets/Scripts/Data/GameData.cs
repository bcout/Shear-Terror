public static class GameData
{
    public static string SHORT_LEVEL_DATA_PATH { get; }
    public static string LONG_LEVEL_DATA_PATH { get; }
    static GameData()
    {
        string base_path = "/LevelData/";
        SHORT_LEVEL_DATA_PATH = base_path + "short_levels.csv";
        LONG_LEVEL_DATA_PATH = base_path + "long_levels.csv";
    }
}
