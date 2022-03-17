﻿using System.Collections.Generic;
using UnityEngine;

public class RunningState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;
    private Animator animator;

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
        animator.SetBool(Animator.StringToHash("IsRunning"), true);
    }

    public void Exit()
    {
        animator.SetBool(Animator.StringToHash("IsRunning"), false);
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
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
