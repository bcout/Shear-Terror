using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private SheepController sheep_controller;

    private float movement_speed;
    private float t;

    private bool coroutine_available;

    private void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
    }

    public void StartFollowingLevel()
    {
        t = 0f;
        coroutine_available = true;
    }

    public void UpdateMovementSpeed()
    {
        GameObject current_block = sheep_controller.GetCurrentBlock();
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
        if (coroutine_available)
        {
            GameObject current_block = sheep_controller.GetCurrentBlock();

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
        coroutine_available = false;
        SheepState state = sheep_controller.GetState();
        CurveData curve_data;
        Vector3 next_point;

        float change = Constants.ROTATION_CHANGE_IN_TURNS;

        if (sheep_controller.GetCurrentBlock().CompareTag(Constants.LEFT_TURN_TAG))
        {
            change = -change;
        }

        float end_angle = transform.eulerAngles.y + change;
        float start_angle = transform.eulerAngles.y;
        float turn_angle;

        while (t < 1
               && (state == (SheepState)sheep_controller.GetRunningState() || state == (SheepState)sheep_controller.GetJumpingState()))
        {
            t += Time.deltaTime * movement_speed;

            curve_data = sheep_controller.GetCurrentLane().GetComponent<CurveData>();
            next_point = curve_data.GetNextPoint(t);
            turn_angle = Mathf.Lerp(start_angle, end_angle, Mathf.SmoothStep(0.0f, 1.0f, t));

            transform.eulerAngles = new Vector3(0, turn_angle, 0);
            transform.position = new Vector3(next_point.x, sheep_controller.GetVerticalPosition(), next_point.z);

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        GoToNextBlock();
        coroutine_available = true;
    }

    private IEnumerator FollowStraight()
    {
        coroutine_available = false;
        Vector3 start_point;
        Vector3 end_point;
        Vector3 next_point;
        SheepState state = sheep_controller.GetState();

        while (t < 1
               && (state == (SheepState)sheep_controller.GetRunningState() || state == (SheepState)sheep_controller.GetJumpingState()))
        {
            t += Time.deltaTime * movement_speed;
            start_point = sheep_controller.GetCurrentLane().transform.Find(Constants.LANE_START_NAME).position;
            end_point = sheep_controller.GetCurrentLane().transform.Find(Constants.LANE_END_NAME).position;
            next_point = Vector3.Lerp(start_point, end_point, t);

            transform.position = new Vector3(next_point.x, sheep_controller.GetVerticalPosition(), next_point.z);

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        GoToNextBlock();
        coroutine_available = true;
    }

    private void GoToNextBlock()
    {
        int current_block_index = sheep_controller.GetCurrentBlockIndex();
        List<GameObject> blocks_in_level = sheep_controller.GetBlocksInLevel();

        current_block_index++;
        sheep_controller.SetCurrentBlockIndex(current_block_index);

        if (current_block_index < blocks_in_level.Count)
        {
            sheep_controller.SetCurrentBlock(blocks_in_level[current_block_index]);
            sheep_controller.UpdateLane();
        }
        else
        {
            sheep_controller.SetState(sheep_controller.GetIdleState());
            coroutine_available = false;
        }
    }
}
