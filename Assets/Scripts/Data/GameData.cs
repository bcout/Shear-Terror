using UnityEngine;
public static class GameData
{
    public static KeyCode jump_key { get; set; }
    public static KeyCode move_left_key { get; set; }
    public static KeyCode move_right_key { get; set; }
    public static string level_parent_name { get; }

    static GameData()
    {
        jump_key = KeyCode.UpArrow;
        level_parent_name = "Level Parent";
    }
}
