// GENERATED AUTOMATICALLY FROM 'Assets/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""8c0f9216-203e-4a77-b998-1bc23b8ba568"",
            ""actions"": [
                {
                    ""name"": ""Mouse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e966e370-7c30-4dba-b85f-bbcbdda1853c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftButtonPress"",
                    ""type"": ""Button"",
                    ""id"": ""83de6234-85c2-4a99-9451-d8a3ac96db03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""LeftButtonHold"",
                    ""type"": ""Button"",
                    ""id"": ""c01c9b0a-1564-4874-8a17-c225cbbc6b6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a8c6307f-5e36-47c1-a6b1-3ad30a80dfb7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2618721-ac7d-4d1c-a2a4-1c3ac1d7cb46"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftButtonPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3374a618-b942-4927-bc44-432b9ab6ee3c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftButtonHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Mouse = m_Gameplay.FindAction("Mouse", throwIfNotFound: true);
        m_Gameplay_LeftButtonPress = m_Gameplay.FindAction("LeftButtonPress", throwIfNotFound: true);
        m_Gameplay_LeftButtonHold = m_Gameplay.FindAction("LeftButtonHold", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Mouse;
    private readonly InputAction m_Gameplay_LeftButtonPress;
    private readonly InputAction m_Gameplay_LeftButtonHold;
    public struct GameplayActions
    {
        private @Input m_Wrapper;
        public GameplayActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse => m_Wrapper.m_Gameplay_Mouse;
        public InputAction @LeftButtonPress => m_Wrapper.m_Gameplay_LeftButtonPress;
        public InputAction @LeftButtonHold => m_Wrapper.m_Gameplay_LeftButtonHold;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Mouse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @LeftButtonPress.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftButtonPress;
                @LeftButtonPress.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftButtonPress;
                @LeftButtonPress.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftButtonPress;
                @LeftButtonHold.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftButtonHold;
                @LeftButtonHold.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftButtonHold;
                @LeftButtonHold.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftButtonHold;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @LeftButtonPress.started += instance.OnLeftButtonPress;
                @LeftButtonPress.performed += instance.OnLeftButtonPress;
                @LeftButtonPress.canceled += instance.OnLeftButtonPress;
                @LeftButtonHold.started += instance.OnLeftButtonHold;
                @LeftButtonHold.performed += instance.OnLeftButtonHold;
                @LeftButtonHold.canceled += instance.OnLeftButtonHold;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMouse(InputAction.CallbackContext context);
        void OnLeftButtonPress(InputAction.CallbackContext context);
        void OnLeftButtonHold(InputAction.CallbackContext context);
    }
}
