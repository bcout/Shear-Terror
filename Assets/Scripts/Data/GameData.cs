using UnityEngine;
public static class GameData
{
    public static KeyCode jump_key { get; set; }
    public static KeyCode move_left_key { get; set; }
    public static KeyCode move_right_key { get; set; }
    public static KeyCode spin_key { get; set; }

    static GameData()
    {
        jump_key = KeyCode.UpArrow;
        move_left_key = KeyCode.LeftArrow;
        move_right_key = KeyCode.RightArrow;
        spin_key = KeyCode.DownArrow;
    }
}
