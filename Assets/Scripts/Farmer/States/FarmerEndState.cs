using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerEndState : MonoBehaviour, FarmerState
{
    private FarmerController farmer_controller;
    private FarmerMovementController farmer_movement_controller;

    public void StateUpdate()
    {

    }

    public void Exit()
    {
        farmer_controller.StopAnimation(Constants.FARM_END_ANIM);
    }

    public void Enter()
    {
        LoadLocalComponents();
        farmer_controller.StartAnimation(Constants.FARM_END_ANIM);
        StartCoroutine(farmer_movement_controller.MoveToMiddle());
    }

    private void LoadLocalComponents()
    {
        farmer_controller = GetComponent<FarmerController>();
        farmer_movement_controller = GetComponent<FarmerMovementController>();
    }

}
