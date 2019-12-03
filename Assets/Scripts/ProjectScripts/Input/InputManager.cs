using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    [SerializeField] PlayerInput m_myPlayerInput;
    PlayerInputAsset m_myInputAsset;
    [SerializeField] GameObject mirilla;
    bool m_gameIsPaused = false;
    private void Start()
    {
        m_myInputAsset = new PlayerInputAsset();
        m_myInputAsset.Enable();
        Cursor.visible = false;
    }
    public Vector2 RotationInput()
    {
        if (!m_gameIsPaused)
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
        }
        return new Vector2(0, 0);
    }
    public float HorizontalMovement()
    {
        if (!m_gameIsPaused)
        {
            return m_myInputAsset.PlayerInputActions.HorizontalMovement.ReadValue<float>();
        }
        return 0;
    }
    public float VerticalMovement()
    {
        if (!m_gameIsPaused)
        {
            return m_myInputAsset.PlayerInputActions.VerticalMovement.ReadValue<float>();
        }
        return 0;
    }
    public bool DashButtonTriggered()
    {
        if (!m_gameIsPaused)
        {
            return m_myInputAsset.PlayerInputActions.DashButton.triggered;
        }
        return false;
        
    }
    public float LeftTrigger()
    {
        if (!m_gameIsPaused)
        {
            return m_myInputAsset.PlayerInputActions.LeftTrigger.ReadValue<float>();
        }
        return 0;
    }
    public float RightTrigger()
    {
        if (!m_gameIsPaused)
        {
            return m_myInputAsset.PlayerInputActions.RightTrigger.ReadValue<float>();
        }
        return 0;
    }
    public bool MapButtonTriggered()
    {
        return m_myInputAsset.PlayerInputActions.MapButton.triggered;
    }
    public bool PauseButtonTriggered()
    {
        return m_myInputAsset.PlayerInputActions.PauseButton.triggered;
    }
    public void PauseGameInputs()
    {
        m_gameIsPaused = true;
    }
    public void UnpauseGameInputs()
    {
        m_gameIsPaused = false;
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
