using UnityEngine;

public class RagdollToggle : MonoBehaviour
{
    //private Animator animator;
    private Rigidbody rigid_body;
    private CapsuleCollider capsule_collider;
    private SheepController sheep_controller;

    private Collider[] children_colliders;
    private Rigidbody[] children_rigid_bodies;

    // Start is called before the first frame update
    void Awake()
    {
        LoadLocalComponents();
        LoadChildComponents();
    }

    private void Start()
    {
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool active)
    {
        foreach (Collider collider in children_colliders)
        {
            collider.enabled = active;
        }
        
        foreach (Rigidbody body in children_rigid_bodies)
        {
            body.detectCollisions = active;
            body.isKinematic = !active;
        }

        //animator.enabled = !active;
        rigid_body.detectCollisions = !active;
        rigid_body.isKinematic = active;
        capsule_collider.enabled = !active;
        sheep_controller.enabled = !active;
    }

    private void LoadLocalComponents()
    {
        //animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody>();
        capsule_collider = GetComponent<CapsuleCollider>();
        sheep_controller = GetComponent<SheepController>();
    }

    private void LoadChildComponents()
    {
        children_colliders = GetComponentsInChildren<Collider>();
        children_rigid_bodies = GetComponentsInChildren<Rigidbody>();
    }
}
