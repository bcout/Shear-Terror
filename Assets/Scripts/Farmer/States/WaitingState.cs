using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : MonoBehaviour, FarmerState
{
    private FarmerController farmer_controller;

    public void StateUpdate()
    {

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
    }
}
