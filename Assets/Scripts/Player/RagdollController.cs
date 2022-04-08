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
    private FootstepPlayer sheep_footstep_player;
    private Camera sheep_camera;
    private Transform sheep_pivot;
    private Transform ragdoll_pivot;
    private Transform ragdoll_position;

    private FarmerController farmer_controller;
    private FarmerMovementController farmer_movement_controller;

    private bool initialized = false;

    public void Init(GameObject sheep_reference, GameObject farmer_reference)
    {
        sheep = sheep_reference;
        farmer = farmer_reference;
        LoadComponents();
        FindTransforms();
        AddForce();

        initialized = true;
        PauseRunners();
    }

    private void Update()
    {
        if (initialized && !GameData.game_paused)
        {
            HandleInput();
        }

        //this needs to be done smoothly
        Vector3 lookDirection = ragdoll_position.position - sheep_camera.transform.position;
        lookDirection.Normalize();
        sheep_camera.transform.rotation = Quaternion.Slerp(sheep_camera.transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResumeRunners();
            Destroy(gameObject);
            GameObject obstacle = sheep_controller.GetCollidedObstacle();
            if (obstacle != null)
            {
                Destroy(obstacle);
                sheep_controller.SetCollidedObstacle(null);
            }
        }
        if (Input.GetKey(GameData.look_back_key))
        {
            sheep_controller.LookBack();
        }
        else
        {
            sheep_controller.LookForward();
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
        sheep_camera = sheep.GetComponentInChildren<Camera>();
        sheep_footstep_player = sheep.GetComponent<FootstepPlayer>();
    }

    private void FindTransforms()
    {
        sheep_pivot = sheep.transform.Find(Constants.PIVOT);
        ragdoll_pivot = transform.Find(Constants.PIVOT);
        ragdoll_position = transform.Find(Constants.ARMATURE);
    }

    private void AddForce()
    {
        ragdoll_pivot.localRotation = sheep_pivot.localRotation;
        Rigidbody[] ragdoll_rb = transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in ragdoll_rb) {
            rb.AddForce(sheep.transform.forward * Constants.RAGDOLL_FORCE);
        }
    }

    private void PauseRunners()
    {
        sheep_collider.enabled = false;
        sheep_renderer.enabled = false;
        sheep_footstep_player.EnableFootstepSounds(false);
        sheep_movement_controller.StopAllCoroutines();
        farmer_movement_controller.StopAllCoroutines();
        farmer_controller.StopAnimation(Constants.FARM_RUN_ANIM);
        farmer_controller.StartAnimation(Constants.FARM_WAIT_ANIM);
    }

    private void ResumeRunners()
    {
        sheep_collider.enabled = true;
        sheep_renderer.enabled = true;
        sheep_footstep_player.EnableFootstepSounds(true);
        sheep_camera.transform.localRotation = Quaternion.Euler(Constants.CAMERA_X_ROTATION, 0f, 0f);
        sheep_controller.Respawn();
        farmer_controller.Respawn();
        farmer_controller.StopAnimation(Constants.FARM_WAIT_ANIM);
        farmer_controller.StartAnimation(Constants.FARM_RUN_ANIM);
    }
}
