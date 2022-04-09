using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private SheepController sheep_controller;
    private Transform body;

    private float movement_speed;
    private float turn_start_angle;
    private float t_jump;
    private float t_trick;
    private float t_end;

    private bool move_coroutine_available;
    private bool jump_coroutine_available;
    private bool trick_coroutine_available;

    private void Start()
    {
        LoadComponents();
        turn_start_angle = 0f;
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        body = transform.Find(Constants.PIVOT);
    }

    public void StartFollowingLevel()
    {
        t_jump = 0f;
        GameData.sheep_t_run = 0f;
        t_trick = 0f;
        t_end = 0f;
        move_coroutine_available = true;
        jump_coroutine_available = true;
        trick_coroutine_available = true;
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
        if (move_coroutine_available)
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
    public IEnumerator MoveToEnd()
    {
        List<GameObject> blocks_in_level = sheep_controller.GetBlocksInLevel();
        Vector3 start_point = transform.position;
        Vector3 end_point = blocks_in_level[blocks_in_level.Count - 1].transform.Find(Constants.LANE_END_NAME).position;
        Vector3 next_point;

         while (t_end < 1)
         {
            t_end += Time.deltaTime * 3f * Constants.BASE_MOVEMENT_SPEED;
            
            next_point = Vector3.Lerp(start_point, end_point, t_end);
            transform.position = new Vector3(next_point.x, sheep_controller.GetVerticalPosition(), next_point.z);

            yield return new WaitForEndOfFrame();
         }

         t_end = 0f;
    }

    public void ContinueJump()
    {
        if(jump_coroutine_available)
        {
            StartCoroutine(Jump());
        }
    }

    public void ContinueTrick()
    {
        if(trick_coroutine_available && PlayerData.curr_trick != PlayerData.Trick.NONE)
        {
            StartCoroutine(Trick(PlayerData.curr_trick));
        }
    }

    private IEnumerator Trick(PlayerData.Trick trick)
    {
        trick_coroutine_available = false;
        Quaternion inital = body.localRotation;

        float start_angle = 0f;
        float end_angle = 0f;
        float turn_angle;

        switch(trick)
        {
            case PlayerData.Trick.LEFT_SPIN:
                start_angle = body.localRotation.y;
                end_angle = body.localRotation.y - Constants.SPIN_ROTATION;
                break;
            case PlayerData.Trick.RIGHT_SPIN:
                start_angle = body.localRotation.y;
                end_angle = body.localRotation.y + Constants.SPIN_ROTATION;
                break;
            case PlayerData.Trick.FRONT_FLIP:
                start_angle = body.localRotation.x;
                end_angle = body.localRotation.x + Constants.SPIN_ROTATION;
                break;
            case PlayerData.Trick.BACK_FLIP:
                start_angle = body.localRotation.x;
                end_angle = body.localRotation.x - Constants.SPIN_ROTATION;
                break;
            case PlayerData.Trick.BARREL_ROLL_LEFT:
                start_angle = body.localRotation.z;
                end_angle = body.localRotation.z - Constants.SPIN_ROTATION;
                break;
            case PlayerData.Trick.BARREL_ROLL_RIGHT:
                start_angle = body.localRotation.z;
                end_angle = body.localRotation.z + Constants.SPIN_ROTATION;
                break;
        }

        while (t_trick < 1)
        {
            t_trick += Time.deltaTime * Constants.SPIN_SPEED;
            turn_angle = Mathf.Lerp(start_angle, end_angle, EaseOut(t_trick));

            switch (trick)
            {
                case PlayerData.Trick.LEFT_SPIN:
                case PlayerData.Trick.RIGHT_SPIN:
                    body.localRotation = inital * Quaternion.Euler(body.rotation.x, turn_angle, body.rotation.z);
                    break;
                case PlayerData.Trick.FRONT_FLIP:
                case PlayerData.Trick.BACK_FLIP:
                    body.localRotation = inital * Quaternion.Euler(turn_angle, body.rotation.y, body.rotation.z);
                    break;
                case PlayerData.Trick.BARREL_ROLL_LEFT:
                case PlayerData.Trick.BARREL_ROLL_RIGHT:
                    body.localRotation = inital * Quaternion.Euler(body.rotation.x, body.rotation.y, turn_angle);
                    break;
            }

            SheepState state = sheep_controller.GetState();
            if (state != (SheepState)sheep_controller.GetEndState() && PlayerData.on_ground && t_trick < Constants.MIN_ROTATION_TO_LAND)
            {
                sheep_controller.GetSoundEffectsPlayer().PlayFootstepSound();
                sheep_controller.SetState(sheep_controller.GetRagdollState());
            }

            yield return new WaitForEndOfFrame();
        }
        t_trick = 0f;
        body.localRotation = inital;

        PlayerData.curr_trick = PlayerData.Trick.NONE;
        trick_coroutine_available = true;
    }

    private IEnumerator Jump()
    {
        PlayerData.on_ground = false;
        jump_coroutine_available = false;

        while (t_jump < 1)
        {
            t_jump += Time.deltaTime * Constants.JUMP_SPEED;
            sheep_controller.SetVerticalPosition(Mathf.Lerp(0, Constants.JUMP_HEIGHT, EaseOut(t_jump)));
            yield return new WaitForEndOfFrame();
        }
        t_jump = 0f;

        while (t_jump < 1)
        {
            t_jump += Time.deltaTime * Constants.JUMP_SPEED;
            sheep_controller.SetVerticalPosition(Mathf.Lerp(Constants.JUMP_HEIGHT, 0, EaseIn(t_jump)));
            yield return new WaitForEndOfFrame();
        }
        t_jump = 0f;

        SheepState state = sheep_controller.GetState();
        if (state != (SheepState)sheep_controller.GetEndState())
        {
            sheep_controller.SetState(sheep_controller.GetRunningState());
        }

        PlayerData.on_ground = true;
        jump_coroutine_available = true;
    }

    private static float EaseOut(float value)
    {
        //quartic ease out
        return 1 - (1 - value) * (1 - value);
    }

    private static float EaseIn(float value)
    {
        //quartic ease in
        return value*value;
    }


    private IEnumerator FollowCurve()
    {
        move_coroutine_available = false;
        SheepState state = sheep_controller.GetState();
        CurveData curve_data;
        Vector3 next_point;

        if (GameData.sheep_t_run == 0)
        {
            turn_start_angle = transform.eulerAngles.y;
        }

        float change = Constants.ROTATION_CHANGE_IN_TURNS;

        if (sheep_controller.GetCurrentBlock().CompareTag(Constants.LEFT_TURN_TAG))
        {
            change = -change;
        }

        float end_angle = turn_start_angle + change;
        float turn_angle;

        while (GameData.sheep_t_run < 1
               && (state == (SheepState)sheep_controller.GetRunningState() || state == (SheepState)sheep_controller.GetJumpingState()))
        {
            GameData.sheep_t_run += Time.deltaTime * movement_speed;

            curve_data = sheep_controller.GetCurrentLane().GetComponent<CurveData>();
            next_point = curve_data.GetNextPoint(GameData.sheep_t_run);
            turn_angle = Mathf.Lerp(turn_start_angle, end_angle, Mathf.SmoothStep(0.0f, 1.0f, GameData.sheep_t_run));

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, turn_angle, transform.eulerAngles.z);
            transform.position = new Vector3(next_point.x, sheep_controller.GetVerticalPosition(), next_point.z);

            yield return new WaitForEndOfFrame();
        }

        GameData.sheep_t_run = 0f;
        GoToNextBlock();
        move_coroutine_available = true;
    }

    private IEnumerator FollowStraight()
    {
        move_coroutine_available = false;
        Vector3 start_point;
        Vector3 end_point;
        Vector3 next_point;
        SheepState state = sheep_controller.GetState();

        while (GameData.sheep_t_run < 1
               && (state == (SheepState)sheep_controller.GetRunningState() || state == (SheepState)sheep_controller.GetJumpingState()) || state == (SheepState)sheep_controller.GetEndState())
        {
            GameData.sheep_t_run += Time.deltaTime * movement_speed;
            start_point = sheep_controller.GetCurrentLane().transform.Find(Constants.LANE_START_NAME).position;
            end_point = sheep_controller.GetCurrentLane().transform.Find(Constants.LANE_END_NAME).position;
            next_point = Vector3.Lerp(start_point, end_point, GameData.sheep_t_run);

            transform.position = new Vector3(next_point.x, sheep_controller.GetVerticalPosition(), next_point.z);

            yield return new WaitForEndOfFrame();
        }

        GameData.sheep_t_run = 0f;
        GoToNextBlock();
        move_coroutine_available = true;
    }
    private void GoToNextBlock()
    {
        int current_block_index = sheep_controller.GetCurrentBlockIndex();
        List<GameObject> blocks_in_level = sheep_controller.GetBlocksInLevel();

        current_block_index++;
        sheep_controller.SetCurrentBlockIndex(current_block_index);

        if (current_block_index < blocks_in_level.Count - 1)
        {
            sheep_controller.SetCurrentBlock(blocks_in_level[current_block_index]);
            sheep_controller.UpdateLane();
        }
        else
        {
            sheep_controller.SetState(sheep_controller.GetEndState());
            move_coroutine_available = false;
        }

        if (current_block_index == blocks_in_level.Count - 3)
        {
            sheep_controller.SpawnLevelEndField();
        }

        sheep_controller.DestroyPreviousBlocks();
    }

    public void SetCoroutineAvailability(bool value)
    {
        move_coroutine_available = value;
        trick_coroutine_available = value;
        jump_coroutine_available = value;

        t_trick = 0f;
        t_jump = 0f;
    }
}
