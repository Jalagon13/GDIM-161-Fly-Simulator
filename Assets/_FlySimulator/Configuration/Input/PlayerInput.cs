//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_FlySimulator/Configuration/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Human"",
            ""id"": ""da6330d6-17ff-4c52-9caa-bf9bcd54ccc4"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""36b21101-facc-4b8d-abc1-97b13513971c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f0783841-0b33-4431-8a3c-84afd510dd34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9af9f48f-e884-4a68-bc96-facdc51bf6ed"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1a52cc5f-a3e6-412d-800e-5d083fc1de6b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""588455e0-9e2f-4d44-a9be-453faf319945"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""94434de9-e8a4-4bd6-b528-babfd65873cc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""851058ba-3e21-49c5-8ea0-e69e5d743dab"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a7082979-4246-4139-886b-3f0db6316123"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4411bf16-36ef-4a31-ab1d-b07754315144"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5c316bf2-d8ae-4157-bc29-a644eacd9dac"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23602b6b-b9d3-454d-872e-306ebab0e98b"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a97ab57e-5572-4ad9-aba9-f58c6a56a44e"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Fly"",
            ""id"": ""c1dfe994-4df5-41a6-86e0-88c2d6af2401"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""7e0f0c59-b8c4-4ec0-958a-dc48ea0a2719"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f3d1257a-c6f5-4cb0-a2d0-dbd4ad284e52"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Human
        m_Human = asset.FindActionMap("Human", throwIfNotFound: true);
        m_Human_HorizontalMovement = m_Human.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_Human_Jump = m_Human.FindAction("Jump", throwIfNotFound: true);
        m_Human_MouseX = m_Human.FindAction("MouseX", throwIfNotFound: true);
        m_Human_MouseY = m_Human.FindAction("MouseY", throwIfNotFound: true);
        // Fly
        m_Fly = asset.FindActionMap("Fly", throwIfNotFound: true);
        m_Fly_Newaction = m_Fly.FindAction("New action", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Human
    private readonly InputActionMap m_Human;
    private List<IHumanActions> m_HumanActionsCallbackInterfaces = new List<IHumanActions>();
    private readonly InputAction m_Human_HorizontalMovement;
    private readonly InputAction m_Human_Jump;
    private readonly InputAction m_Human_MouseX;
    private readonly InputAction m_Human_MouseY;
    public struct HumanActions
    {
        private @PlayerInput m_Wrapper;
        public HumanActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_Human_HorizontalMovement;
        public InputAction @Jump => m_Wrapper.m_Human_Jump;
        public InputAction @MouseX => m_Wrapper.m_Human_MouseX;
        public InputAction @MouseY => m_Wrapper.m_Human_MouseY;
        public InputActionMap Get() { return m_Wrapper.m_Human; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HumanActions set) { return set.Get(); }
        public void AddCallbacks(IHumanActions instance)
        {
            if (instance == null || m_Wrapper.m_HumanActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_HumanActionsCallbackInterfaces.Add(instance);
            @HorizontalMovement.started += instance.OnHorizontalMovement;
            @HorizontalMovement.performed += instance.OnHorizontalMovement;
            @HorizontalMovement.canceled += instance.OnHorizontalMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @MouseX.started += instance.OnMouseX;
            @MouseX.performed += instance.OnMouseX;
            @MouseX.canceled += instance.OnMouseX;
            @MouseY.started += instance.OnMouseY;
            @MouseY.performed += instance.OnMouseY;
            @MouseY.canceled += instance.OnMouseY;
        }

        private void UnregisterCallbacks(IHumanActions instance)
        {
            @HorizontalMovement.started -= instance.OnHorizontalMovement;
            @HorizontalMovement.performed -= instance.OnHorizontalMovement;
            @HorizontalMovement.canceled -= instance.OnHorizontalMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @MouseX.started -= instance.OnMouseX;
            @MouseX.performed -= instance.OnMouseX;
            @MouseX.canceled -= instance.OnMouseX;
            @MouseY.started -= instance.OnMouseY;
            @MouseY.performed -= instance.OnMouseY;
            @MouseY.canceled -= instance.OnMouseY;
        }

        public void RemoveCallbacks(IHumanActions instance)
        {
            if (m_Wrapper.m_HumanActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IHumanActions instance)
        {
            foreach (var item in m_Wrapper.m_HumanActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_HumanActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public HumanActions @Human => new HumanActions(this);

    // Fly
    private readonly InputActionMap m_Fly;
    private List<IFlyActions> m_FlyActionsCallbackInterfaces = new List<IFlyActions>();
    private readonly InputAction m_Fly_Newaction;
    public struct FlyActions
    {
        private @PlayerInput m_Wrapper;
        public FlyActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Fly_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Fly; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FlyActions set) { return set.Get(); }
        public void AddCallbacks(IFlyActions instance)
        {
            if (instance == null || m_Wrapper.m_FlyActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_FlyActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IFlyActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IFlyActions instance)
        {
            if (m_Wrapper.m_FlyActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IFlyActions instance)
        {
            foreach (var item in m_Wrapper.m_FlyActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_FlyActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public FlyActions @Fly => new FlyActions(this);
    public interface IHumanActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
    }
    public interface IFlyActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
