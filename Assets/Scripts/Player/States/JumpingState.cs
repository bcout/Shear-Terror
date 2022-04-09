using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;

    public void StateUpdate()
    {
        if (!GameData.game_paused)
        {
            HandleInput();
        }
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
        movement_controller.ContinueJump();
        movement_controller.ContinueTrick();
    }

    public void Enter()
    {
        LoadLocalComponents();
        sheep_controller.StartAnimation(Constants.JUMP_UP_ANIM);
        PlayerData.curr_trick = PlayerData.Trick.NONE;
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.JUMP_UP_ANIM);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(GameData.flip_front_key))
        {
            SetTrick(PlayerData.Trick.FRONT_FLIP);
        }
        if (Input.GetKeyDown(GameData.flip_back_key))
        {
            SetTrick(PlayerData.Trick.BACK_FLIP);
        }
        if (Input.GetKeyDown(GameData.roll_left_key))
        {
            SetTrick(PlayerData.Trick.BARREL_ROLL_LEFT);
        }
        if (Input.GetKeyDown(GameData.roll_right_key))
        {
            SetTrick(PlayerData.Trick.BARREL_ROLL_RIGHT);
        }
        if (Input.GetKeyDown(GameData.spin_left_key))
        {
            SetTrick(PlayerData.Trick.LEFT_SPIN);
        }
        if (Input.GetKeyDown(GameData.spin_right_key))
        {
            SetTrick(PlayerData.Trick.RIGHT_SPIN);
        }
    }

    private void SetTrick(PlayerData.Trick new_trick)
    {
        PlayerData.curr_trick = new_trick;
    }

    private void LoadLocalComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
    }
}
