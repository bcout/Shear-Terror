using System.Collections.Generic;
using UnityEngine;

public class RunningState : MonoBehaviour, SheepState
{
    private SheepController parent;

    public void StateUpdate()
    {
        HandleInput();
        parent.UpdateMovementSpeed();
        parent.MoveAlongPath();
    }

    public void Enter()
    {
        print("RUNNING");
        LoadComponents();
    }

    public void Exit()
    {

    }

    private void LoadComponents()
    {
        parent = GetComponent<SheepController>();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(GameData.move_left_key))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(GameData.move_right_key))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(GameData.jump_key))
        {
            // Transition to jump state
        }
    }

    private void MoveLeft()
    {
        switch (PlayerData.curr_lane)
        {
            case PlayerData.Lanes.LEFT:
                break;
            case PlayerData.Lanes.MIDDLE:
                PlayerData.curr_lane = PlayerData.Lanes.LEFT;
                break;
            case PlayerData.Lanes.RIGHT:
                PlayerData.curr_lane = PlayerData.Lanes.MIDDLE;
                break;
        }

        parent.UpdateLane();
    }

    private void MoveRight()
    {
        switch (PlayerData.curr_lane)
        {
            case PlayerData.Lanes.LEFT:
                PlayerData.curr_lane = PlayerData.Lanes.MIDDLE;
                break;
            case PlayerData.Lanes.MIDDLE:
                PlayerData.curr_lane = PlayerData.Lanes.RIGHT;
                break;
            case PlayerData.Lanes.RIGHT:
                break;
        }

        parent.UpdateLane();
    }
}
