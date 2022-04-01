using UnityEngine;
using UnityEngine.SceneManagement;

public class EndState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;

    public void StateUpdate()
    {

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
