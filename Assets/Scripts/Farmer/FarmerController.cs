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

    private List<GameObject> blocks_in_level;

    private void Start()
    {
        LoadStates();
        LoadComponents();
        SetDefaultState(waiting_state);
    }

    public void StartLevel()
    {
        blocks_in_level = sheep.GetComponent<SheepController>().GetBlocksInLevel();

    }

    private void Update()
    {
        state.StateUpdate();
    }

    private void LoadStates()
    {
        chasing_state = GetComponent<ChasingState>();
        waiting_state = GetComponent<WaitingState>();
        end_state = GetComponent<FarmerEndState>();
    }

    private void LoadComponents()
    {
        animator = GetComponent<Animator>();
        farmer_movement_controller = GetComponent<FarmerMovementController>();
    }

    public void StopAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), false);
    }

    public void StartAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), true);
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
    #endregion
}
