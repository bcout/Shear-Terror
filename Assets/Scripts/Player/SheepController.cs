using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    private SheepState state;
    private RunningState running_state;
    private IdleState idle_state;
    private JumpingState jumping_state;
    private RagdollState ragdoll_state;

    private List<GameObject> blocks_in_level;
    private GameObject current_block;
    private GameObject current_lane;
    private GameObject level_parent;

    private int current_block_index;

    private float movement_speed;
    private float t;
    private float y_value;

    private bool coroutine_available;

    private void Start()
    {
        LoadStates();
        SetDefaultState(idle_state);
        y_value = 0;
    }

    private void Update()
    {
        state.StateUpdate();
    }

    // Called by GameController
    public void StartLevel()
    {
        ReadLevelBlocks();
        StartFollowingLevel();
        SetState(running_state);
    }

    private void ReadLevelBlocks()
    {
        level_parent = GameObject.Find(GameData.level_parent_name);
        blocks_in_level = new List<GameObject>();

        for (int i = 0; i < level_parent.transform.childCount; i++)
        {
            blocks_in_level.Add(level_parent.transform.GetChild(i).gameObject);
        }

        current_block_index = 0;
    }

    private void StartFollowingLevel()
    {
        current_block = blocks_in_level[current_block_index];
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;

        t = 0f;
        coroutine_available = true;
    }

    public void UpdateMovementSpeed()
    {
        switch (current_block.tag)
        {
            case Constants.SHORT_STRAIGHT_TAG:
                movement_speed = 3 * Constants.BASE_MOVEMENT_SPEED;
                break;
            case Constants.LONG_STRAIGHT_TAG:
                movement_speed = 1.5f * Constants.BASE_MOVEMENT_SPEED;
                break;
            case Constants.TURN_TAG:
                movement_speed = Constants.BASE_MOVEMENT_SPEED;
                break;
        }
    }

    public void MoveAlongPath()
    {
        if (coroutine_available)
        {
            switch (current_block.tag)
            {
                case Constants.TURN_TAG:
                    StartCoroutine(FollowCurve());
                    break;
                case Constants.SHORT_STRAIGHT_TAG:
                case Constants.LONG_STRAIGHT_TAG:
                    StartCoroutine(FollowStraight());
                    break;
            }
        }
        
    }

    public void UpdateLane()
    {
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
    }

    private IEnumerator FollowCurve()
    {
        coroutine_available = false;
        CurveData curve_data;
        Vector3 next_point;

        while (t < 1
               && ((RunningState)state == running_state || (JumpingState)state == jumping_state))
        {
            t += Time.deltaTime * movement_speed;

            curve_data = current_lane.GetComponent<CurveData>();
            next_point = curve_data.GetNextPoint(t);

            transform.position = next_point;

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

        while (t < 1
               && ((RunningState)state == running_state || (JumpingState)state == jumping_state))
        {
            t += Time.deltaTime * movement_speed;
            start_point = current_lane.transform.Find(Constants.LANE_START_NAME).position;
            end_point = current_lane.transform.Find(Constants.LANE_END_NAME).position;
            next_point = Vector3.Lerp(start_point, end_point, t);

            transform.position = new Vector3(next_point.x, y_value, next_point.z);

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
            current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
        }
    }

    public void SetState(SheepState new_state)
    {
        state.Exit();
        state = new_state;
        state.Enter();
    }

    private void SetDefaultState(SheepState default_state)
    {
        state = default_state;
        state.Enter();
    }

    private void LoadStates()
    {
        running_state = GetComponent<RunningState>();
        idle_state = GetComponent<IdleState>();
        jumping_state = GetComponent<JumpingState>();
        ragdoll_state = GetComponent<RagdollState>();
    }
}