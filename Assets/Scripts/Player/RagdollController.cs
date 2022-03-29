using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private GameObject sheep;
    private GameObject farmer;

    private SheepController sheep_controller;
    private MovementController sheep_movement_controller;
    private CapsuleCollider sheep_collider;
    private SkinnedMeshRenderer sheep_renderer;
    private FarmerController farmer_controller;
    private FarmerMovementController farmer_movement_controller;

    private bool initialized = false;

    public void Init(GameObject sheep_reference, GameObject farmer_reference)
    {
        sheep = sheep_reference;
        farmer = farmer_reference;
        LoadComponents();

        initialized = true;

        PauseRunners();
    }

    private void Update()
    {
        if (initialized)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResumeRunners();
                Destroy(gameObject);
            }
        }
    }

    private void LoadComponents()
    {
        sheep_controller = sheep.GetComponent<SheepController>();
        farmer_controller = farmer.GetComponent<FarmerController>();
        sheep_movement_controller = sheep.GetComponent<MovementController>();
        farmer_movement_controller = farmer.GetComponent<FarmerMovementController>();

        sheep_collider = sheep.GetComponent<CapsuleCollider>();
        sheep_renderer = sheep.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void PauseRunners()
    {
        sheep_collider.enabled = false;
        sheep_renderer.enabled = false;
        sheep_movement_controller.StopAllCoroutines();
        farmer_movement_controller.StopAllCoroutines();
    }

    private void ResumeRunners()
    {
        sheep_collider.enabled = true;
        sheep_renderer.enabled = true;
        sheep_controller.Respawn();
        farmer_controller.Respawn();
    }
}
