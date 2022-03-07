﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class SheepMovement : MonoBehaviour
{
    private float movement_speed;

    private GameObject game_controller;
    private List<GameObject> blocks_in_level;
    private GameObject current_block;
    private GameObject current_lane;
    private int current_block_index;

    private float t;
    private bool coroutine_available;

    private void Awake()
    {
        // PlayerInputActions is the c# file generated by the new input system, using the input maps/actions defined in the unity editor
        PlayerInputActions player_input_actions = new PlayerInputActions();

        // Enable the player action map
        player_input_actions.Player.Enable();

        // Subscribe to the performed event of each input action
        player_input_actions.Player.Jump.performed += Jump;
        player_input_actions.Player.MoveLeft.performed += MoveLeft;
        player_input_actions.Player.MoveRight.performed += MoveRight;
        player_input_actions.Player.MoveLeft.performed += ChangeLane;
        player_input_actions.Player.MoveRight.performed += ChangeLane;
    }

    private void Start()
    {
        game_controller = GameObject.Find("Game Controller");
        GameObject level_parent = GameObject.Find("Level Parent");

        blocks_in_level = new List<GameObject>();
        for (int i = 0; i < level_parent.transform.childCount; i++)
        {
            blocks_in_level.Add(level_parent.transform.GetChild(i).gameObject);
        }

        // Start the player off on the first block in their current lane
        current_block_index = 0;
        current_block = blocks_in_level[current_block_index];
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
        
        t = 0f;
        coroutine_available = true;

    }

    private void Update()
    {
        if (current_block.name == "Short Straight(Clone)")
        {
            movement_speed = 3 * Constants.BASE_MOVEMENT_SPEED;
        }
        else if (current_block.name == "Long Straight(Clone)")
        {
            movement_speed = 2 * Constants.BASE_MOVEMENT_SPEED;
        }
        else
        {
            movement_speed = Constants.BASE_MOVEMENT_SPEED;
        }

        if (current_block.CompareTag("Turn"))
        {
            if (coroutine_available)
            {
                StartCoroutine(FollowCurve());
            }
        }
        else if (current_block.CompareTag("Straight"))
        {
            if (coroutine_available)
            {
                StartCoroutine(FollowStraight());
            }
        }
    }

    private IEnumerator FollowStraight()
    {
        coroutine_available = false;
        while (t < 1)
        {
            t += Time.deltaTime * movement_speed;   
            Vector3 start_point = current_lane.transform.Find("Start").position;
            Vector3 end_point = current_lane.transform.Find("End").position;
            Vector3 next_point = Vector3.Lerp(start_point, end_point, t);

            transform.position = next_point;

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        GoToNextBlock();
        coroutine_available = true;
    }

    private IEnumerator FollowCurve()
    {
        coroutine_available = false;
        while (t < 1)
        {
            t += Time.deltaTime * movement_speed;

            CurveData curve_data = current_lane.GetComponent<CurveData>();
            Vector3 next_point = curve_data.GetNextPoint(t);

            //transform.LookAt(next_point);
            transform.position = next_point;
            //transform.position = Vector3.MoveTowards(transform.position, next_point, Time.deltaTime * movement_speed);

            yield return new WaitForEndOfFrame();
        }

        t = 0f;

        GoToNextBlock();
        coroutine_available = true;
    }

    private void GoToNextBlock()
    {
        current_block_index++;
        if (current_block_index < blocks_in_level.Count)
        {
            current_block = blocks_in_level[current_block_index];
            //print(current_block_index);
            //print(current_block);
            current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
        }
        
    }

    private void Jump(InputAction.CallbackContext context)
    {
        print("Jump");
    }

    private void MoveLeft(InputAction.CallbackContext context)
    {
        //print("Move left");
        switch (PlayerData.curr_lane)
        {
            case PlayerData.Lanes.LEFT:
                // Do nothing
                break;
            case PlayerData.Lanes.MIDDLE:
                PlayerData.curr_lane = PlayerData.Lanes.LEFT;
                break;
            case PlayerData.Lanes.RIGHT:
                PlayerData.curr_lane = PlayerData.Lanes.MIDDLE;
                break;
        }
    }

    private void MoveRight(InputAction.CallbackContext context)
    {
        //print("Move right");
        switch (PlayerData.curr_lane)
        {
            case PlayerData.Lanes.LEFT:
                PlayerData.curr_lane = PlayerData.Lanes.MIDDLE;
                break;
            case PlayerData.Lanes.MIDDLE:
                PlayerData.curr_lane = PlayerData.Lanes.RIGHT;
                break;
            case PlayerData.Lanes.RIGHT:
                // Do nothing
                break;
        }
    }

    private void ChangeLane(InputAction.CallbackContext context)
    {
        // TESTING
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
        //
    }
}
