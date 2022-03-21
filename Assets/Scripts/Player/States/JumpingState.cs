using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;

    public void StateUpdate()
    {
        HandleInput();
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
        movement_controller.ContinueJump();
        movement_controller.ContinueTrick();
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.StartAnimation(Constants.JUMP_UP_ANIM);
        PlayerData.curr_trick = PlayerData.Trick.NONE;
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.JUMP_UP_ANIM);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(GameData.spin_key))
        {
            Spin();
        }
    }

    private void Spin()
    {
        PlayerData.curr_trick = PlayerData.Trick.SPIN;
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
    }
}
