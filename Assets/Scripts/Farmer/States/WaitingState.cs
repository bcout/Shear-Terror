using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : MonoBehaviour, FarmerState
{
    [SerializeField]
    private GameObject sheep;

    private FarmerController farmer_controller;
    private SheepController sheep_controller;

    public void StateUpdate()
    {
        if (sheep_controller.GetState() == (SheepState)sheep_controller.GetRunningState()
            || sheep_controller.GetState() == (SheepState)sheep_controller.GetJumpingState())
        {
            StartCoroutine(StartDelayed());
        }
    }

    private IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        farmer_controller.SetState(farmer_controller.GetChasingState());
    }

    public void Exit()
    {
        farmer_controller.StopAnimation(Constants.FARM_WAIT_ANIM);
    }

    public void Enter()
    {
        LoadComponents();
        farmer_controller.StartAnimation(Constants.FARM_WAIT_ANIM);
    }

    private void LoadComponents()
    {
        farmer_controller = GetComponent<FarmerController>();
        sheep_controller = sheep.GetComponent<SheepController>();
    }
}
