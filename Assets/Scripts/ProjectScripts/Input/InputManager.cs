using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    [SerializeField] PlayerInput m_myPlayerInput;
    PlayerInputAsset m_myInputAsset;
    [SerializeField] GameObject mirilla;

    private void Start()
    {
        m_myInputAsset = new PlayerInputAsset();
        m_myInputAsset.Enable();
        Cursor.visible = false;
    }
    public Vector2 RotationInput()
    {
        if (m_myPlayerInput.currentControlScheme == "Gamepad")
        {
            StartControllingWithGamepad();
            return m_myInputAsset.PlayerInputActions.Rotating.ReadValue<Vector2>();
        }
        if (m_myPlayerInput.currentControlScheme == "Keyboard & Mouse")
        {
            StartControllingWithMouse();
            Vector2 screenMouse = m_myInputAsset.PlayerInputActions.MousePos.ReadValue<Vector2>();
            screenMouse = Camera.main.ScreenToWorldPoint(screenMouse);
            mirilla.transform.position = screenMouse; //Reposiciona la mirilla
            return (screenMouse - (Vector2)GameManager.Instance.ActualPlayerController.armObject.transform.position).normalized;
        }
        return new Vector2(0, 0);
    }
    void StartControllingWithMouse()
    {
        GameManager.Instance.ActualPlayerController.DeactivateAutoAim();//Activa el auto aim
        mirilla.SetActive(true); //activa la mirilla
        GameManager.Instance.realPlayerGO.GetComponent<PossessAbility>().StartControllingWithMouse();
    }
    void StartControllingWithGamepad()
    {
        GameManager.Instance.ActualPlayerController.ActivateAutoAim(); //Desactiva el autoaim
        mirilla.SetActive(false); //Desactiva la mirilla
        GameManager.Instance.realPlayerGO.GetComponent<PossessAbility>().StopControllingWithMouse();
    }
}
