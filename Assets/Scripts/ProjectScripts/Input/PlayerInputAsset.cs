// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/ProjectScripts/Input/PlayerInputAsset.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerInputAsset : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public PlayerInputAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAsset"",
    ""maps"": [
        {
            ""name"": ""PlayerInputActions"",
            ""id"": ""473fff24-6755-417d-b234-034d6afe8e34"",
            ""actions"": [
                {
                    ""name"": ""Horizontal Movement"",
                    ""type"": ""Value"",
                    ""id"": ""d69c8ed0-9f72-4290-a571-c7952896513c"",
                    ""expectedControlType"": """",
                    ""processors"": ""AxisDeadzone(min=0.25,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical Movement"",
                    ""type"": ""Value"",
                    ""id"": ""4490aab5-6922-4bf1-a7f2-3b35fca6bc6b"",
                    ""expectedControlType"": """",
                    ""processors"": ""AxisDeadzone(min=0.25,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotating"",
                    ""type"": ""Value"",
                    ""id"": ""a05b8957-02cf-4fa8-831f-bd40bbb8fd78"",
                    ""expectedControlType"": """",
                    ""processors"": ""StickDeadzone(min=0.5,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightTrigger"",
                    ""type"": ""Value"",
                    ""id"": ""4e23a992-b586-419d-9af6-5cb15872b8d9"",
                    ""expectedControlType"": """",
                    ""processors"": ""AxisDeadzone(min=0.3,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftTrigger"",
                    ""type"": ""Value"",
                    ""id"": ""7cdfa9bb-df7a-4ea2-904d-10800515b0fd"",
                    ""expectedControlType"": """",
                    ""processors"": ""AxisDeadzone(min=0.3,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton"",
                    ""type"": ""Button"",
                    ""id"": ""5125c569-5545-4da9-9b00-b55063819c95"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DashButton"",
                    ""type"": ""Button"",
                    ""id"": ""54740f2a-d73c-41c0-82b3-95d6ddc7c304"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseButton"",
                    ""type"": ""Button"",
                    ""id"": ""4c98ca9e-1907-4c20-850d-5f277b467e32"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MapButton"",
                    ""type"": ""Button"",
                    ""id"": ""16173212-37ff-4e49-bfaa-ffc67277fbdf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Ps4"",
                    ""id"": ""2a95925d-b254-4f8c-911f-22baeb596dc5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""84a3b883-f605-4115-b651-e3ee643a3aa2"",
                    ""path"": ""<DualShockGamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""3aab8d88-b580-4fea-a800-137ed1c15d9a"",
                    ""path"": ""<DualShockGamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""X Box"",
                    ""id"": ""35e22be9-1d87-44f8-b83a-54d3f2401ddc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""909deaee-611c-4ee3-9da4-a0d2d695005a"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""7e528f21-3e57-4031-b93e-8b5f150f8f71"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Ps4"",
                    ""id"": ""fbc6bfbf-aa14-4664-aa76-6395206a027b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cc70a595-5875-4f70-a74f-e94e5762f043"",
                    ""path"": ""<DualShockGamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c031590d-915e-4ab9-9e7c-1a4cd057d3fd"",
                    ""path"": ""<DualShockGamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""X Box"",
                    ""id"": ""733d5b47-e1d5-4caf-bc75-f6e3b3166e57"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9bd62d8f-18f7-4651-b2d4-28fc7c5f9858"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b542296d-7f56-47a7-902f-08a641e1337d"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bde11b5c-0aa0-4853-99c5-3df7c3cd2851"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31031dd7-ce89-44d3-b8f0-aef8201a31a8"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80cea14d-df45-4934-81bf-416cb03b667c"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5d0c28a-5ba4-4dc1-9904-6fc3fd178d34"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b39c3d7b-9161-40f0-88f8-e361f6a7577a"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""085842f2-27a8-4e64-893e-582b262f7b41"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2fc7fc47-e57c-4ca9-96c2-8a07b743e8bb"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3092b850-9870-4e03-a8ef-04396ff3c8e5"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97c47e92-f163-4fdd-8a33-1a3b253c340a"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8b8d10e-839d-4a09-adf5-e85a06fef767"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""500d442b-3354-4763-84a8-d0ac76e1cb77"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d1fa79e-4c81-4346-b683-ff5fce88a060"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""951a10cb-8824-4e57-b1f6-87b106082b74"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37c492b4-65e3-4679-9741-731df93553fc"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInputActions
        m_PlayerInputActions = asset.FindActionMap("PlayerInputActions", throwIfNotFound: true);
        m_PlayerInputActions_HorizontalMovement = m_PlayerInputActions.FindAction("Horizontal Movement", throwIfNotFound: true);
        m_PlayerInputActions_VerticalMovement = m_PlayerInputActions.FindAction("Vertical Movement", throwIfNotFound: true);
        m_PlayerInputActions_Rotating = m_PlayerInputActions.FindAction("Rotating", throwIfNotFound: true);
        m_PlayerInputActions_RightTrigger = m_PlayerInputActions.FindAction("RightTrigger", throwIfNotFound: true);
        m_PlayerInputActions_LeftTrigger = m_PlayerInputActions.FindAction("LeftTrigger", throwIfNotFound: true);
        m_PlayerInputActions_ActionButton = m_PlayerInputActions.FindAction("ActionButton", throwIfNotFound: true);
        m_PlayerInputActions_DashButton = m_PlayerInputActions.FindAction("DashButton", throwIfNotFound: true);
        m_PlayerInputActions_PauseButton = m_PlayerInputActions.FindAction("PauseButton", throwIfNotFound: true);
        m_PlayerInputActions_MapButton = m_PlayerInputActions.FindAction("MapButton", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerInputActions
    private readonly InputActionMap m_PlayerInputActions;
    private IPlayerInputActionsActions m_PlayerInputActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerInputActions_HorizontalMovement;
    private readonly InputAction m_PlayerInputActions_VerticalMovement;
    private readonly InputAction m_PlayerInputActions_Rotating;
    private readonly InputAction m_PlayerInputActions_RightTrigger;
    private readonly InputAction m_PlayerInputActions_LeftTrigger;
    private readonly InputAction m_PlayerInputActions_ActionButton;
    private readonly InputAction m_PlayerInputActions_DashButton;
    private readonly InputAction m_PlayerInputActions_PauseButton;
    private readonly InputAction m_PlayerInputActions_MapButton;
    public struct PlayerInputActionsActions
    {
        private PlayerInputAsset m_Wrapper;
        public PlayerInputActionsActions(PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_PlayerInputActions_HorizontalMovement;
        public InputAction @VerticalMovement => m_Wrapper.m_PlayerInputActions_VerticalMovement;
        public InputAction @Rotating => m_Wrapper.m_PlayerInputActions_Rotating;
        public InputAction @RightTrigger => m_Wrapper.m_PlayerInputActions_RightTrigger;
        public InputAction @LeftTrigger => m_Wrapper.m_PlayerInputActions_LeftTrigger;
        public InputAction @ActionButton => m_Wrapper.m_PlayerInputActions_ActionButton;
        public InputAction @DashButton => m_Wrapper.m_PlayerInputActions_DashButton;
        public InputAction @PauseButton => m_Wrapper.m_PlayerInputActions_PauseButton;
        public InputAction @MapButton => m_Wrapper.m_PlayerInputActions_MapButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActionsActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsActionsCallbackInterface != null)
            {
                HorizontalMovement.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnHorizontalMovement;
                HorizontalMovement.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnHorizontalMovement;
                HorizontalMovement.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnHorizontalMovement;
                VerticalMovement.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnVerticalMovement;
                VerticalMovement.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnVerticalMovement;
                VerticalMovement.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnVerticalMovement;
                Rotating.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnRotating;
                Rotating.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnRotating;
                Rotating.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnRotating;
                RightTrigger.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnRightTrigger;
                RightTrigger.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnRightTrigger;
                RightTrigger.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnRightTrigger;
                LeftTrigger.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnLeftTrigger;
                LeftTrigger.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnLeftTrigger;
                LeftTrigger.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnLeftTrigger;
                ActionButton.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnActionButton;
                ActionButton.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnActionButton;
                ActionButton.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnActionButton;
                DashButton.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnDashButton;
                DashButton.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnDashButton;
                DashButton.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnDashButton;
                PauseButton.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnPauseButton;
                PauseButton.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnPauseButton;
                PauseButton.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnPauseButton;
                MapButton.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnMapButton;
                MapButton.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnMapButton;
                MapButton.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnMapButton;
            }
            m_Wrapper.m_PlayerInputActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                HorizontalMovement.started += instance.OnHorizontalMovement;
                HorizontalMovement.performed += instance.OnHorizontalMovement;
                HorizontalMovement.canceled += instance.OnHorizontalMovement;
                VerticalMovement.started += instance.OnVerticalMovement;
                VerticalMovement.performed += instance.OnVerticalMovement;
                VerticalMovement.canceled += instance.OnVerticalMovement;
                Rotating.started += instance.OnRotating;
                Rotating.performed += instance.OnRotating;
                Rotating.canceled += instance.OnRotating;
                RightTrigger.started += instance.OnRightTrigger;
                RightTrigger.performed += instance.OnRightTrigger;
                RightTrigger.canceled += instance.OnRightTrigger;
                LeftTrigger.started += instance.OnLeftTrigger;
                LeftTrigger.performed += instance.OnLeftTrigger;
                LeftTrigger.canceled += instance.OnLeftTrigger;
                ActionButton.started += instance.OnActionButton;
                ActionButton.performed += instance.OnActionButton;
                ActionButton.canceled += instance.OnActionButton;
                DashButton.started += instance.OnDashButton;
                DashButton.performed += instance.OnDashButton;
                DashButton.canceled += instance.OnDashButton;
                PauseButton.started += instance.OnPauseButton;
                PauseButton.performed += instance.OnPauseButton;
                PauseButton.canceled += instance.OnPauseButton;
                MapButton.started += instance.OnMapButton;
                MapButton.performed += instance.OnMapButton;
                MapButton.canceled += instance.OnMapButton;
            }
        }
    }
    public PlayerInputActionsActions @PlayerInputActions => new PlayerInputActionsActions(this);
    public interface IPlayerInputActionsActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnVerticalMovement(InputAction.CallbackContext context);
        void OnRotating(InputAction.CallbackContext context);
        void OnRightTrigger(InputAction.CallbackContext context);
        void OnLeftTrigger(InputAction.CallbackContext context);
        void OnActionButton(InputAction.CallbackContext context);
        void OnDashButton(InputAction.CallbackContext context);
        void OnPauseButton(InputAction.CallbackContext context);
        void OnMapButton(InputAction.CallbackContext context);
    }
}
