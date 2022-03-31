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
        if (initialized)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResumeRunners();
                Destroy(gameObject);
            }
        }

        //this needs to be done smoothly
        Vector3 lookDirection = ragdoll_position.position - sheep_camera.transform.position;
        lookDirection.Normalize();
        sheep_camera.transform.rotation = Quaternion.Slerp(sheep_camera.transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime);
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
        sheep_movement_controller.StopAllCoroutines();
        farmer_movement_controller.StopAllCoroutines();
    }

    private void ResumeRunners()
    {
        sheep_collider.enabled = true;
        sheep_renderer.enabled = true;
        sheep_camera.transform.localRotation = Quaternion.Euler(Constants.CAMERA_X_ROTATION, 0f, 0f);
        sheep_controller.Respawn();
        farmer_controller.Respawn();
    }
}
