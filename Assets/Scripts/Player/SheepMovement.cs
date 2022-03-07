using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{

    private void Awake()
    {
        PlayerInputActions player_input_actions = new PlayerInputActions();
        // Enable the player action map
        player_input_actions.Player.Enable();

        // Subscribe to the performed event of each input action
        player_input_actions.Player.Jump.performed += Jump;
        player_input_actions.Player.MoveLeft.performed += MoveLeft;
        player_input_actions.Player.MoveRight.performed += MoveRight;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        print("Jump");
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        print("Move left");
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        print("Move right");
    }
}
