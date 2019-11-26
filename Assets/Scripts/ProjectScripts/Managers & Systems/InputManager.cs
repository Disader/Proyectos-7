﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    [SerializeField] PlayerInput playerinput;
    PlayerInputAsset actions;
    private void Start()
    {
        actions = new PlayerInputAsset();
        actions.Enable();
  
    }

    private void Update()
    {

        HorizontalMovement();
    }
    public void HorizontalMovement()
    {
        if (Keyboard.current != null && Mouse.current != null)
        {
            if (actions.PlayerInputActions.HorizontalMovement.ReadValue<float>() > 0)
            {
                Debug.Log("keyboard");
            }


        }
        else if (Gamepad.current != null)
        {
            if (actions.PlayerInputActions.HorizontalMovement.ReadValue<float>()>0)
            {
                Debug.Log("Gamepad");
            }
           
        } 
    }
  
}
