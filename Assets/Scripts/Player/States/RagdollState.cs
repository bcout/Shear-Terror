using UnityEngine;

public class RagdollState : MonoBehaviour, SheepState
{
    [SerializeField] private GameObject sheep_ragdoll;
    [SerializeField] private GameObject farmer;

    public void StateUpdate()
    {

    }

    public void Enter()
    {
        SpawnRagdoll();
    }

    public void Exit()
    {

    }

    private void SpawnRagdoll()
    {
        GameObject ragdoll = Instantiate(sheep_ragdoll, transform.position, transform.rotation);
        ragdoll.GetComponent<RagdollController>().Init(gameObject, farmer);
    }
}

