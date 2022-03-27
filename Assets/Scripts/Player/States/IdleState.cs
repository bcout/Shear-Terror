using UnityEngine;

public class IdleState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;

    public void StateUpdate()
    {
        LoadComponents();
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.StartAnimation(Constants.IDLE_ANIM);
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.IDLE_ANIM);
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
    }
}
