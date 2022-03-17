using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;

    public void StateUpdate()
    {
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.StartAnimation(Constants.JUMP_UP_ANIM);
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.JUMP_UP_ANIM);
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
    }
}
