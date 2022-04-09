using System.Collections.Generic;
using UnityEngine;

public class RunningState : MonoBehaviour, SheepState
{
    [SerializeField]
    private GameObject lane_change_trigger_prefab;

    private SheepController sheep_controller;
    private MovementController movement_controller;

    public void StateUpdate()
    {
        if (!GameData.game_paused)
        {
            HandleInput();
        }
        movement_controller.UpdateMovementSpeed();
        movement_controller.MoveAlongPath();
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.StartAnimation(Constants.RUN_ANIM);
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.RUN_ANIM);
        sheep_controller.LookForward();
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(GameData.move_left_key))
        {
            if (!GameData.isGameOver)
            {
                MoveLeft();
            }

        }
        if (Input.GetKeyDown(GameData.move_right_key))
        {
            if (!GameData.isGameOver)
            {
                MoveRight();
            }
        }
        if (Input.GetKeyDown(GameData.jump_key))
        {
            if (!GameData.isGameOver)
            {
                sheep_controller.SetState(sheep_controller.GetJumpingState());
            }
        }
        if (Input.GetKey(GameData.look_back_key))
        {
            sheep_controller.LookBack();
        }
        else
        {
            sheep_controller.LookForward();
        }
    }

    private void MoveLeft()
    {
        bool spawn_trigger = false;
        switch (PlayerData.curr_lane)
        {
            case PlayerData.Lane.LEFT:
                break;
            case PlayerData.Lane.MIDDLE:
                PlayerData.curr_lane = PlayerData.Lane.LEFT;
                spawn_trigger = true;
                break;
            case PlayerData.Lane.RIGHT:
                PlayerData.curr_lane = PlayerData.Lane.MIDDLE;
                spawn_trigger = true;
                break;
        }
        if (spawn_trigger)
        {
            Vector3 spawn_point = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            GameObject lane_change_trigger = Instantiate(lane_change_trigger_prefab, spawn_point, transform.rotation);
            lane_change_trigger.GetComponent<LaneChangeTrigger>().SetDirection(false);
        }
        
        sheep_controller.UpdateLane();
    }

    private void MoveRight()
    {
        bool spawn_trigger = false;
        switch (PlayerData.curr_lane)
        {
            case PlayerData.Lane.LEFT:
                PlayerData.curr_lane = PlayerData.Lane.MIDDLE;
                spawn_trigger = true;
                break;
            case PlayerData.Lane.MIDDLE:
                PlayerData.curr_lane = PlayerData.Lane.RIGHT;
                spawn_trigger = true;
                break;
            case PlayerData.Lane.RIGHT:
                break;
        }

        if (spawn_trigger)
        {
            Vector3 spawn_point = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            GameObject lane_change_trigger = Instantiate(lane_change_trigger_prefab, spawn_point, transform.rotation);
            lane_change_trigger.GetComponent<LaneChangeTrigger>().SetDirection(true);
        }
        

        sheep_controller.UpdateLane();
    }
}
