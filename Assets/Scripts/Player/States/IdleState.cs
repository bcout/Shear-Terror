using UnityEngine;

public class IdleState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;

    public void StateUpdate()
    {

    }

    public void Enter()
    {
        LoadLocalComponents();
        sheep_controller.StartAnimation(Constants.IDLE_ANIM);
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.IDLE_ANIM);
    }

    private void LoadLocalComponents()
    {
        sheep_controller = GetComponent<SheepController>();
    }
}
