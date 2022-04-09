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
    public static float sheep_t_run { get; set; }
    public static float farmer_t_run { get; set; }
    public static int level_pool_id { get; set; }
    
    public static int sheepLivesRemaining { get; set; }
    
    public static int sheepLivesInitial { get; }
    
    public static int current_level { get; set; }

    public static bool isGameOver { get; set; }
    
    public static bool game_paused { get; set; }
    public static bool currently_going_to_main_menu { get; set; }
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

        level_pool_id = Constants.SHORT_LEVEL_ID;

        sheepLivesInitial = 5;
        sheepLivesRemaining = sheepLivesInitial;

        isGameOver = false;
        game_paused = false;

        current_level = 1;
        currently_going_to_main_menu = false;
    }
}
