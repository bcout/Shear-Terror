using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : MonoBehaviour, FarmerState
{
    private FarmerController farmer_controller;

    public void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            farmer_controller.SetState(farmer_controller.GetEndState());
        }
    }

    public void Exit()
    {
        farmer_controller.StopAnimation(Constants.FARM_RUN_ANIM);
    }

    public void Enter()
    {
        LoadComponents();
        farmer_controller.StartAnimation(Constants.FARM_RUN_ANIM);
    }

    private void LoadComponents()
    {
        farmer_controller = GetComponent<FarmerController>();
    }
}
