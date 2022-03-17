using System.Collections.Generic;
using UnityEngine;

public class RunningState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;

    public void StateUpdate()
    {
        HandleInput();
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
    }

    public void Enter()
    {
        print("RUNNING");
        LoadComponents();
        sheep_controller.StartAnimation(Constants.RUN_ANIM);
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.RUN_ANIM);
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
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
            sheep_controller.SetState(sheep_controller.GetJumpingState());
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

        sheep_controller.UpdateLane();
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

        sheep_controller.UpdateLane();
    }
}
