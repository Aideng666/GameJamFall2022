using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction moveAction1;
    InputAction strikeAction1;
    InputAction dodgeAction1;
    InputAction moveAction2;
    InputAction strikeAction2;
    InputAction dodgeAction2;

    public static InputManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        
        playerInput.actions.FindActionMap("Player1").Enable();
        playerInput.actions.FindActionMap("Player2").Enable();

        moveAction1 = playerInput.actions["Move"];
        strikeAction1 = playerInput.actions["Strike"];
        dodgeAction1 = playerInput.actions["Dodge"];
        moveAction2 = playerInput.actions["Move2"];
        strikeAction2 = playerInput.actions["Strike2"];
        dodgeAction2 = playerInput.actions["Dodge2"];
    }

    public Vector2 Move1()
    {
        return moveAction1.ReadValue<Vector2>();
    }

    public bool Strike1()
    {
        return strikeAction1.triggered;
    }

    public bool Dodge1()
    {
        return dodgeAction1.triggered;
    }

    public Vector2 Move2()
    {
        return moveAction2.ReadValue<Vector2>();
    }

    public bool Strike2()
    {
        return strikeAction2.triggered;
    }

    public bool Dodge2()
    {
        return dodgeAction2.triggered;
    }
}
