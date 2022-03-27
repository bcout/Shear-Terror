using UnityEngine;

public class StartState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;

    public void StateUpdate()
    {

        // Press any key to begin
        if (Input.anyKeyDown)
        {
            sheep_controller.SetState(sheep_controller.GetRunningState());
        }
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.StartAnimation(Constants.IDLE_ANIM);

        transform.position = sheep_controller.GetCurrentBlock().transform.Find(Constants.BLOCK_CENTER_NAME).position;
        transform.rotation = sheep_controller.GetCurrentBlock().transform.Find(Constants.BLOCK_CENTER_NAME).rotation;
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
