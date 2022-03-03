using UnityEngine;

/*
 * This class will hold data like what level the player is on, which blocks can be used to generate levels, 
 * their high score, etc.
 * Just basic overall data that pertains to the game at a high level
 */
public class GameData : MonoBehaviour
{
    /*
     * An array of all the level block prefabs that can be used to make a level.
     * Its elements are set in the inspector by dragging the desired prefabs into the slots.
     * If you want a certain block to be used to generate the level, drag it into this array.
     */
    public GameObject[] available_level_blocks;
}
