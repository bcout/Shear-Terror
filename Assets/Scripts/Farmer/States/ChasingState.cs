using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : MonoBehaviour, FarmerState
{
    private FarmerController farmer_controller;
    private FarmerMovementController farmer_movement_controller;

    public void StateUpdate()
    {
        farmer_movement_controller.UpdateMovementSpeed();
        farmer_movement_controller.MoveAlongPath();
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
        farmer_movement_controller = GetComponent<FarmerMovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.LANE_CHANGE_TRIGGER_TAG))
        {
            bool move_right = other.GetComponent<LaneChangeTrigger>().GetDirection();
            if (move_right)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }

            Destroy(other.gameObject);
        }        
    }

    private void MoveLeft()
    {
        switch (FarmerData.curr_lane)
        {
            case FarmerData.Lane.LEFT:
                break;
            case FarmerData.Lane.MIDDLE:
                FarmerData.curr_lane = FarmerData.Lane.LEFT;
                break;
            case FarmerData.Lane.RIGHT:
                FarmerData.curr_lane = FarmerData.Lane.MIDDLE;
                break;
        }

        farmer_controller.UpdateLane();
    }

    private void MoveRight()
    {
        switch (FarmerData.curr_lane)
        {
            case FarmerData.Lane.LEFT:
                FarmerData.curr_lane = FarmerData.Lane.MIDDLE;
                break;
            case FarmerData.Lane.MIDDLE:
                FarmerData.curr_lane = FarmerData.Lane.RIGHT;
                break;
            case FarmerData.Lane.RIGHT:
                break;
        }

        farmer_controller.UpdateLane();
    }
}
