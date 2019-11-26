using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    [SerializeField] PlayerInput playerinput;
    PlayerInputAsset actions;
    [SerializeField] InputActionAsset asset;
    
    private void Start()
    {
        actions = new PlayerInputAsset();
        actions.Enable();
    }

    private void Update()
    {
        if (playerinput.currentControlScheme == "Keyboard & Mouse")
        {
            
           
                
        }
        HorizontalMovement();
    }
    public void HorizontalMovement()
    {
        if (Gamepad.current == null)
        {
            if (actions.PlayerInputActions.HorizontalMovement.ReadValue<float>() > 0)
            {
                Debug.Log("keyboard");
            }


        }
        else
        {
            if (actions.PlayerInputActions.HorizontalMovement.ReadValue<float>()>0)
            {
                Debug.Log("Gamepad");
            }
           
        } 
    }
  
}
