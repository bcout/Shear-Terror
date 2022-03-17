using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;
    private Animator animator;

    public void StateUpdate()
    {
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.SetVerticalPosition(5f);
        animator.SetBool(Animator.StringToHash("IsJumping"), true);
    }

    public void Exit()
    {
        animator.SetBool(Animator.StringToHash("IsJumping"), false);
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
    }
}
