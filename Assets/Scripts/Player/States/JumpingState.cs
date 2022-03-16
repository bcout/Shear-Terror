using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : MonoBehaviour, SheepState
{
    private SheepController parent;

    public void StateUpdate()
    {
        print("JUMP!");
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
