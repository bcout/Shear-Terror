using UnityEngine;

public class IdleState : MonoBehaviour, SheepState
{
    private SheepController parent;

    public void StateUpdate()
    {
        print("IDLE");
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
