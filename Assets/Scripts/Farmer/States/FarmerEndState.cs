using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerEndState : MonoBehaviour, FarmerState
{
    private FarmerController farmer_controller;

    public void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            farmer_controller.SetState(farmer_controller.GetWaitingState());
        }
    }

    public void Exit()
    {
        farmer_controller.StopAnimation(Constants.FARM_END_ANIM);
    }

    public void Enter()
    {
        LoadComponents();
        farmer_controller.StartAnimation(Constants.FARM_END_ANIM);
    }

    private void LoadComponents()
    {
        farmer_controller = GetComponent<FarmerController>();
    }
}
