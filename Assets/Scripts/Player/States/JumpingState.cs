using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;
    private float move = 50f;

    public void StateUpdate()
    {
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
        sheep_controller.SetVerticalPosition(sheep_controller.GetVerticalPosition() + move * Time.deltaTime);
    }

    public void Enter()
    {
        print("JUMPING");
        LoadComponents();
        sheep_controller.StartAnimation(Constants.JUMP_UP_ANIM);
        MoveUp();
    }

    private void Done()
    {
        sheep_controller.SetState(sheep_controller.GetRunningState());
    }

    private void MoveUp()
    {
        Invoke("MoveDown", 1);
    }

    private void MoveDown()
    {
        move = -move;
        Invoke("Done", 1);
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
