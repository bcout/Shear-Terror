using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovementController : MonoBehaviour
{
    [SerializeField]
    private GameObject sheep;

    private FarmerController farmer_controller;
    private SheepController sheep_controller;

    private float movement_speed;
    private float turn_start_angle;

    private bool move_coroutine_available;

    private void Start()
    {
        LoadComponents();
        turn_start_angle = 0f;
    }

    private void LoadComponents()
    {
        farmer_controller = GetComponent<FarmerController>();
        sheep_controller = sheep.GetComponent<SheepController>();
    }

    public void StartFollowingLevel()
    {
        move_coroutine_available = true;
        GameData.farmer_t_run = 0f;
    }

    public void UpdateMovementSpeed()
    {
        GameObject current_block = farmer_controller.GetCurrentBlock();
        switch (current_block.tag)
        {
            case Constants.SHORT_STRAIGHT_TAG:
                movement_speed = 3 * Constants.BASE_MOVEMENT_SPEED;
                break;
            case Constants.LONG_STRAIGHT_TAG:
                movement_speed = 1.5f * Constants.BASE_MOVEMENT_SPEED;
                break;
            case Constants.LEFT_TURN_TAG:
            case Constants.RIGHT_TURN_TAG:
                movement_speed = Constants.BASE_MOVEMENT_SPEED;
                break;
        }
    }

    public void MoveAlongPath()
    {
        if (move_coroutine_available)
        {
            GameObject current_block = farmer_controller.GetCurrentBlock();

            switch (current_block.tag)
            {
                case Constants.LEFT_TURN_TAG:
                case Constants.RIGHT_TURN_TAG:
                    StartCoroutine(FollowCurve());
                    break;
                case Constants.SHORT_STRAIGHT_TAG:
                case Constants.LONG_STRAIGHT_TAG:
                    StartCoroutine(FollowStraight());
                    break;
            }
        }
    }

    private IEnumerator FollowCurve()
    {
        move_coroutine_available = false;
        FarmerState state = farmer_controller.GetState();
        CurveData curve_data;
        Vector3 next_point;

        if (GameData.farmer_t_run == 0)
        {
            turn_start_angle = transform.eulerAngles.y;
        }

        float change = Constants.ROTATION_CHANGE_IN_TURNS;

        if (farmer_controller.GetCurrentBlock().CompareTag(Constants.LEFT_TURN_TAG))
        {
            change = -change;
        }

        float end_angle = turn_start_angle + change;
        float turn_angle;

        while (GameData.farmer_t_run < 1 && (state == (FarmerState)farmer_controller.GetChasingState()))
        {
            GameData.farmer_t_run += Time.deltaTime * movement_speed;

            curve_data = farmer_controller.GetCurrentLane().GetComponent<CurveData>();
            next_point = curve_data.GetNextPoint(GameData.farmer_t_run);
            turn_angle = Mathf.Lerp(turn_start_angle, end_angle, Mathf.SmoothStep(0.0f, 1.0f, GameData.farmer_t_run));

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, turn_angle, transform.eulerAngles.z);
            transform.position = new Vector3(next_point.x, 0f, next_point.z);

            yield return new WaitForEndOfFrame();
        }

        GameData.farmer_t_run = 0f;
        GoToNextBlock();
        move_coroutine_available = true;
    }

    private IEnumerator FollowStraight()
    {
        move_coroutine_available = false;
        Vector3 start_point;
        Vector3 end_point;
        Vector3 next_point;
        FarmerState state = farmer_controller.GetState();

        while (GameData.farmer_t_run < 1 && (state == (FarmerState)farmer_controller.GetChasingState()))
        {
            GameData.farmer_t_run += Time.deltaTime * movement_speed;
            start_point = farmer_controller.GetCurrentLane().transform.Find(Constants.LANE_START_NAME).position;
            end_point = farmer_controller.GetCurrentLane().transform.Find(Constants.LANE_END_NAME).position;
            next_point = Vector3.Lerp(start_point, end_point, GameData.farmer_t_run);

            transform.position = new Vector3(next_point.x, 0f, next_point.z);

            yield return new WaitForEndOfFrame();
        }

        GameData.farmer_t_run = 0f;
        GoToNextBlock();
        move_coroutine_available = true;
    }

    private void GoToNextBlock()
    {
        int current_block_index = farmer_controller.GetCurrentBlockIndex();
        List<GameObject> blocks_in_level = sheep_controller.GetBlocksInLevel();

        current_block_index++;
        farmer_controller.SetCurrentBlockIndex(current_block_index);

        if (current_block_index < blocks_in_level.Count - 1)
        {
            farmer_controller.SetCurrentBlock(blocks_in_level[current_block_index]);
            farmer_controller.UpdateLane();
        }
        else
        {
            farmer_controller.SetState(farmer_controller.GetEndState());
            move_coroutine_available = false;
        }
    }

    public void SetMovementCoroutineAvailable(bool value)
    {
        move_coroutine_available = value;
    }
}
