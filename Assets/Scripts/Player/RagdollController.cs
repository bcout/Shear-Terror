using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private GameObject sheep;
    private GameObject farmer;

    private SheepController sheep_controller;
    private MovementController sheep_movement_controller;
    private FarmerController farmer_controller;

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
        sheep_movement_controller = sheep.GetComponent<MovementController>();
    }

    private void PauseRunners()
    {
        sheep_movement_controller.StopAllCoroutines();
    }

    private void ResumeRunners()
    {
        //sheep_controller.Respawn();
    }
}
