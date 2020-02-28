// GENERATED AUTOMATICALLY FROM 'Assets/New Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerInputPad"",
            ""id"": ""4b84a683-b9b1-4699-a620-7d6559ad760b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0fd16ebb-cd3b-425c-a371-c57eaccfed07"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""fbaa9c8c-8f1b-4e77-bf62-009db0dd5089"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""bfc57bbd-f634-4372-a3ac-19646b4e5a1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Value"",
                    ""id"": ""67d0915e-a171-4c3c-9bf8-30d2dcbd9669"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""BackDash"",
                    ""type"": ""Button"",
                    ""id"": ""34c2dc58-6053-4c87-b0dd-3fb58fee4ec6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ShieldCharge"",
                    ""type"": ""Button"",
                    ""id"": ""54d94229-4699-4e77-8ec4-4b2a1fa925a8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""608cb228-b795-413f-a184-9a4133de2e18"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c0bb7f0-5ee5-4617-a659-091916e7a3a4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5198e0a5-4a86-4a84-a058-79eb9a9a334f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4d84d87-596d-40a8-8c76-0f2769883729"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""238abc7a-26a8-42c1-9400-e1fbabc2dbdc"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a876261-c33a-4edb-a325-fcf3c2c14b6a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a63b79c-1782-449b-b2d6-f114df99f2d3"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShieldCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ee52c92-78e1-407a-a1aa-799a52f8b4de"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInputPad
        m_PlayerInputPad = asset.FindActionMap("PlayerInputPad", throwIfNotFound: true);
        m_PlayerInputPad_Movement = m_PlayerInputPad.FindAction("Movement", throwIfNotFound: true);
        m_PlayerInputPad_Rotation = m_PlayerInputPad.FindAction("Rotation", throwIfNotFound: true);
        m_PlayerInputPad_Attack = m_PlayerInputPad.FindAction("Attack", throwIfNotFound: true);
        m_PlayerInputPad_Block = m_PlayerInputPad.FindAction("Block", throwIfNotFound: true);
        m_PlayerInputPad_BackDash = m_PlayerInputPad.FindAction("BackDash", throwIfNotFound: true);
        m_PlayerInputPad_ShieldCharge = m_PlayerInputPad.FindAction("ShieldCharge", throwIfNotFound: true);
        m_PlayerInputPad_LockOn = m_PlayerInputPad.FindAction("LockOn", throwIfNotFound: true);
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

    // PlayerInputPad
    private readonly InputActionMap m_PlayerInputPad;
    private IPlayerInputPadActions m_PlayerInputPadActionsCallbackInterface;
    private readonly InputAction m_PlayerInputPad_Movement;
    private readonly InputAction m_PlayerInputPad_Rotation;
    private readonly InputAction m_PlayerInputPad_Attack;
    private readonly InputAction m_PlayerInputPad_Block;
    private readonly InputAction m_PlayerInputPad_BackDash;
    private readonly InputAction m_PlayerInputPad_ShieldCharge;
    private readonly InputAction m_PlayerInputPad_LockOn;
    public struct PlayerInputPadActions
    {
        private @NewControls m_Wrapper;
        public PlayerInputPadActions(@NewControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerInputPad_Movement;
        public InputAction @Rotation => m_Wrapper.m_PlayerInputPad_Rotation;
        public InputAction @Attack => m_Wrapper.m_PlayerInputPad_Attack;
        public InputAction @Block => m_Wrapper.m_PlayerInputPad_Block;
        public InputAction @BackDash => m_Wrapper.m_PlayerInputPad_BackDash;
        public InputAction @ShieldCharge => m_Wrapper.m_PlayerInputPad_ShieldCharge;
        public InputAction @LockOn => m_Wrapper.m_PlayerInputPad_LockOn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputPad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputPadActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputPadActions instance)
        {
            if (m_Wrapper.m_PlayerInputPadActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnMovement;
                @Rotation.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnRotation;
                @Attack.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnAttack;
                @Block.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnBlock;
                @BackDash.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnBackDash;
                @BackDash.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnBackDash;
                @BackDash.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnBackDash;
                @ShieldCharge.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnShieldCharge;
                @ShieldCharge.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnShieldCharge;
                @ShieldCharge.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnShieldCharge;
                @LockOn.started -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerInputPadActionsCallbackInterface.OnLockOn;
            }
            m_Wrapper.m_PlayerInputPadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @BackDash.started += instance.OnBackDash;
                @BackDash.performed += instance.OnBackDash;
                @BackDash.canceled += instance.OnBackDash;
                @ShieldCharge.started += instance.OnShieldCharge;
                @ShieldCharge.performed += instance.OnShieldCharge;
                @ShieldCharge.canceled += instance.OnShieldCharge;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
            }
        }
    }
    public PlayerInputPadActions @PlayerInputPad => new PlayerInputPadActions(this);
    public interface IPlayerInputPadActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnBackDash(InputAction.CallbackContext context);
        void OnShieldCharge(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
    }
}
