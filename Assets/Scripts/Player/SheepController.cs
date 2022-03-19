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

    private MovementController movement_controller;
    private Animator animator;

    private List<GameObject> blocks_in_level;
    private GameObject current_block;
    private GameObject current_lane;
    private GameObject level_parent;

    private int current_block_index;

    private float vertical_position;

    private void Start()
    {
        LoadStates();
        LoadComponents();
        SetDefaultState(idle_state);

        vertical_position = 0f;
    }

    private void Update()
    {
        state.StateUpdate();

        bool run = animator.GetBool(Constants.RUN_ANIM);
        bool jump = animator.GetBool(Constants.JUMP_UP_ANIM);
        bool landing = animator.GetBool(Constants.LANDING_ANIM);
        bool tuck_in = animator.GetBool(Constants.TUCK_IN_ANIM);
        bool tuck_out = animator.GetBool(Constants.TUCK_OUT_ANIM);
        bool idle = animator.GetBool(Constants.IDLE_ANIM);
        bool fall_idle = animator.GetBool(Constants.FALL_IDLE_ANIM);

        string output = "State: " + state + "\n" +
                        "Run: " + run + "\n" + 
                        "Jump Up: " + jump + "\n" +
                        "Landing: " + landing + "\n" +
                        "Tuck In: " + tuck_in + "\n" +
                        "Tuck Out: " + tuck_out + "\n" +
                        "Idle: " + idle + "\n" +
                        "Fall Idle: " + fall_idle + "\n";
        print(output);

    }

    private void LoadComponents()
    {
        movement_controller = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
    }

    // Called by GameController once the level is generated
    public void StartLevel()
    {
        ReadLevelBlocks();
        StartFollowingLevel();
        SetState(running_state);
    }

    private void ReadLevelBlocks()
    {
        level_parent = GameObject.Find(Constants.LEVEL_PARENT_NAME);
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

        movement_controller.StartFollowingLevel();
    }

    public void StopAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), false);
    }

    public void StartAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), true);
    }

    #region Vertical position Get/Set Methods
    public float GetVerticalPosition()
    {
        return vertical_position;
    }

    public void SetVerticalPosition(float value)
    {
        vertical_position = value;
    }
    #endregion

    #region Block Get/Set Methods
    public GameObject GetCurrentBlock()
    {
        return current_block;
    }

    public void SetCurrentBlock(GameObject value)
    {
        current_block = value;
    }

    public int GetCurrentBlockIndex()
    {
        return current_block_index;
    }

    public void SetCurrentBlockIndex(int value)
    {
        current_block_index = value;
    }

    public List<GameObject> GetBlocksInLevel()
    {
        return blocks_in_level;
    }
    #endregion

    #region Lane Get/Set Methods
    public GameObject GetCurrentLane()
    {
        return current_lane;
    }

    public void UpdateLane()
    {
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
    }
    #endregion
    
    #region State Get/Set Methods
    public void SetState(SheepState new_state)
    {
        state.Exit();
        state = new_state;
        state.Enter();
    }

    public SheepState GetState()
    {
        return state;
    }

    public RunningState GetRunningState()
    {
        return running_state;
    }

    public IdleState GetIdleState()
    {
        return idle_state;
    }

    public JumpingState GetJumpingState()
    {
        return jumping_state;
    }

    public RagdollState GetRagdollState()
    {
        return ragdoll_state;
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
    #endregion
}