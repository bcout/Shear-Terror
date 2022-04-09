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
            sheep_controller.GetMusicPlayer().StartMusic();
        }
    }

    public void Enter()
    {
        LoadLocalComponents();
        sheep_controller.StartAnimation(Constants.IDLE_ANIM);

        transform.position = sheep_controller.GetCurrentBlock().transform.Find(Constants.BLOCK_CENTER_NAME).position;
        transform.rotation = sheep_controller.GetCurrentBlock().transform.Find(Constants.BLOCK_CENTER_NAME).rotation;
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
