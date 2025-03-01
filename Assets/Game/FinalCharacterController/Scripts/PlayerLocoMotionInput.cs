using System;
using System.Collections;
using System.Collections.Generic;
using Game.FinalCharacterController;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocoMotionInput : MonoBehaviour, PlayerControls.IPlayerLocoMotionMapActions
{
    
    public PlayerControls PlayerControls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set;}


    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.PlayerLocoMotionMap.Enable();
        PlayerControls.PlayerLocoMotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.PlayerLocoMotionMap.Disable();
        PlayerControls.PlayerLocoMotionMap.RemoveCallbacks(this);
    }

    void PlayerControls.IPlayerLocoMotionMapActions.OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        // print(MovementInput);
    }

    void PlayerControls.IPlayerLocoMotionMapActions.OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
        // print(LookInput);

    }
}
