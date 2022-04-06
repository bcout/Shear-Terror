using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : MonoBehaviour
{
    [SerializeField]
    private GameObject sheep;

    private FarmerState state;
    private ChasingState chasing_state;
    private WaitingState waiting_state;
    private FarmerEndState end_state;

    private Animator animator;
    private FarmerMovementController farmer_movement_controller;
    private SheepController sheep_controller;

    private List<GameObject> blocks_in_level;
    private GameObject current_block;
    private GameObject current_lane;

    private int current_block_index;

    private void Start()
    {
        LoadStates();
        LoadComponents();
        SetDefaultState(waiting_state);
    }

    private void Update()
    {
        state.StateUpdate();
        print(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    private void LoadComponents()
    {
        animator = GetComponent<Animator>();
        farmer_movement_controller = GetComponent<FarmerMovementController>();
        sheep_controller = sheep.GetComponent<SheepController>();
    }

    public void StartLevel()
    {
        blocks_in_level = sheep_controller.GetBlocksInLevel();
        current_block_index = 0;
        StartFollowingLevel();
        SetState(waiting_state);
    }

    private void StartFollowingLevel()
    {
        current_block = blocks_in_level[current_block_index];

        // Start on the same lane as the sheep
        current_lane = sheep_controller.GetCurrentLane();
        FarmerData.SetCurrLane(current_lane);

        farmer_movement_controller.StartFollowingLevel();
    }

    public void StopAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), false);
    }

    public void StartAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), true);
    }

    public GameObject GetCurrentBlock()
    {
        return current_block;
    }

    public void SetCurrentBlock(GameObject new_block)
    {
        current_block = new_block;
    }

    public int GetCurrentBlockIndex()
    {
        return current_block_index;
    }

    public void SetCurrentBlockIndex(int value)
    {
        current_block_index = value;
    }

    public GameObject GetCurrentLane()
    {
        return current_lane;
    }

    public void UpdateLane()
    {
        current_lane = current_block.GetComponent<BlockData>().GetLane(FarmerData.curr_lane).gameObject;
    }

    public FarmerState GetState()
    {
        return state;
    }

    #region State Get Methods
    public ChasingState GetChasingState()
    {
        return chasing_state;
    }

    public WaitingState GetWaitingState()
    {
        return waiting_state;
    }

    public FarmerEndState GetEndState()
    {
        return end_state;
    }

    private void SetDefaultState(FarmerState default_state)
    {
        state = default_state;
        state.Enter();
    }

    public void SetState(FarmerState new_state)
    {
        state.Exit();
        state = new_state;
        state.Enter();
    }

    private void LoadStates()
    {
        chasing_state = GetComponent<ChasingState>();
        waiting_state = GetComponent<WaitingState>();
        end_state = GetComponent<FarmerEndState>();
    }
    #endregion

    public void Respawn()
    {
        SetState(chasing_state);
        farmer_movement_controller.SetMovementCoroutineAvailable(true);
    }
}
