using UnityEngine;
using UnityEngine.InputSystem;

public class SheepController : MonoBehaviour
{
    private SheepState state;
    private RunningState running_state;
    private IdleState idle_state;
    private JumpingState jumping_state;
    private RagdollState ragdoll_state;

    private void Start()
    {
        LoadStates();
        SetDefaultState(idle_state);
    }

    private void Update()
    {
        state.StateUpdate();
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
    }

    public void ChangeLane(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        // If grounded, able to jump, ...
        SetState(jumping_state);
    }

    public void SetState(SheepState new_state)
    {
        state.Exit();
        state = new_state;
        state.Enter();
    }

    private void SetDefaultState(SheepState default_state)
    {
        state = default_state;
        state.Enter();
    }

    private void LoadStates()
    {
        running_state = GetComponent<RunningState>();
        idle_state = GetComponent<IdleState>();
        jumping_state = GetComponent<JumpingState>();
        ragdoll_state = GetComponent<RagdollState>();
    }
}