using UnityEngine;
public static class GameData
{
    public static KeyCode jump_key { get; set; }
    public static KeyCode move_left_key { get; set; }
    public static KeyCode move_right_key { get; set; }
    public static KeyCode look_back_key { get; set; }
    public static KeyCode flip_front_key { get; set; }
    public static KeyCode flip_back_key { get; set; }
    public static KeyCode roll_left_key { get; set; }
    public static KeyCode roll_right_key { get; set; }
    public static KeyCode spin_left_key { get; set; }
    public static KeyCode spin_right_key { get; set; }
    static GameData()
    {
        // jump_key = KeyCode.UpArrow;
        jump_key = KeyCode.UpArrow;
        move_left_key = KeyCode.LeftArrow;
        move_right_key = KeyCode.RightArrow;
        look_back_key = KeyCode.DownArrow;

        flip_front_key = KeyCode.W;
        flip_back_key = KeyCode.S;
        roll_left_key = KeyCode.A;
        roll_right_key = KeyCode.D;
        spin_left_key = KeyCode.Q;
        spin_right_key = KeyCode.E;
    }
}
