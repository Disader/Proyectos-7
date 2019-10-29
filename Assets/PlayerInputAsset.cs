// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputAsset.inputactions'

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
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""d69c8ed0-9f72-4290-a571-c7952896513c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Ps4 Vector2"",
                    ""id"": ""2a95925d-b254-4f8c-911f-22baeb596dc5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""84a3b883-f605-4115-b651-e3ee643a3aa2"",
                    ""path"": ""<DualShockGamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3aab8d88-b580-4fea-a800-137ed1c15d9a"",
                    ""path"": ""<DualShockGamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b1b13d5a-541c-4ab6-9f9d-ea2393c8e72c"",
                    ""path"": ""<DualShockGamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3dc06643-99b0-4826-b259-2a8e47cfb45d"",
                    ""path"": ""<DualShockGamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XBox Vector2"",
                    ""id"": ""35e22be9-1d87-44f8-b83a-54d3f2401ddc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""909deaee-611c-4ee3-9da4-a0d2d695005a"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7e528f21-3e57-4031-b93e-8b5f150f8f71"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5ff14666-a7a3-4088-95aa-fc8a10afb251"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fd40d558-8ac9-4316-ae8c-3daa69284174"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInputActions
        m_PlayerInputActions = asset.FindActionMap("PlayerInputActions", throwIfNotFound: true);
        m_PlayerInputActions_Movement = m_PlayerInputActions.FindAction("Movement", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerInputActions_Movement;
    public struct PlayerInputActionsActions
    {
        private PlayerInputAsset m_Wrapper;
        public PlayerInputActionsActions(PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerInputActions_Movement;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActionsActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerInputActionsActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_PlayerInputActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
            }
        }
    }
    public PlayerInputActionsActions @PlayerInputActions => new PlayerInputActionsActions(this);
    public interface IPlayerInputActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
