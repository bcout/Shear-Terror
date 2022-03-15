using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class SheepMovement : MonoBehaviour
{
    private float movement_speed;
    private float jump_height = 5.0f;

    private GameObject game_controller;
    private List<GameObject> blocks_in_level;
    private GameObject current_block;
    private GameObject current_lane;
    private int current_block_index;

    private float t;
    private bool coroutine_available;

    private bool is_jumping;
    private bool is_grounded;
    private float vertical_speed;

    private void Start()
    {
        /*
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
        */
    }

    private void Update()
    {
        //SetMovementSpeed();

        //FollowBlockPath();
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

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }


    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
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

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
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

    public void ChangeLane(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
    }

    private void SetMovementSpeed()
    {
        if (current_block.name == "Short Straight(Clone)")
        {
            movement_speed = 3 * Constants.BASE_MOVEMENT_SPEED;
        }
        else if (current_block.name == "Long Straight(Clone)")
        {
            movement_speed = 1.5f * Constants.BASE_MOVEMENT_SPEED;
        }
        else
        {
            movement_speed = Constants.BASE_MOVEMENT_SPEED;
        }
    }

    private void FollowBlockPath()
    {
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
}
