﻿using UnityEngine;

public class RagdollState : MonoBehaviour, SheepState
{
    [SerializeField] private GameObject sheep_ragdoll;
    [SerializeField] private GameObject farmer;

    private SheepController sheep_controller;
    [SerializeField] private GameController gameController;

    public void StateUpdate()
    {

    }

    public void Enter()
    {
        LoadComponents();
        SpawnRagdoll();
    }

    public void Exit()
    {

    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
    }

    private void SpawnRagdoll()
    {
        // Detect ragdoll and deduct a life from the player.
        gameController.decrementLife();
        
        GameObject ragdoll = Instantiate(sheep_ragdoll, transform.position, transform.rotation);
        ragdoll.GetComponent<RagdollController>().Init(gameObject, farmer);
    }
}

