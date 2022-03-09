// GENERATED AUTOMATICALLY FROM 'Assets/Thrusters.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Thrusters : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Thrusters()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Thrusters"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""b6b264bd-1d2f-412b-9381-89ece9ea6f9e"",
            ""actions"": [
                {
                    ""name"": ""LeftThruster"",
                    ""type"": ""Value"",
                    ""id"": ""aeb47d97-9d5a-4e29-bd28-833d2ea3356e"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightThruster"",
                    ""type"": ""Value"",
                    ""id"": ""0e6b359b-228a-4f0b-970c-475cea72f34b"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd344e0a-96eb-4acb-8c2c-6e4181537ec4"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightThruster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13943791-bde3-42f1-95f3-f1db04a360c8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftThruster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_LeftThruster = m_Game.FindAction("LeftThruster", throwIfNotFound: true);
        m_Game_RightThruster = m_Game.FindAction("RightThruster", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_LeftThruster;
    private readonly InputAction m_Game_RightThruster;
    public struct GameActions
    {
        private @Thrusters m_Wrapper;
        public GameActions(@Thrusters wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftThruster => m_Wrapper.m_Game_LeftThruster;
        public InputAction @RightThruster => m_Wrapper.m_Game_RightThruster;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @LeftThruster.started -= m_Wrapper.m_GameActionsCallbackInterface.OnLeftThruster;
                @LeftThruster.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnLeftThruster;
                @LeftThruster.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnLeftThruster;
                @RightThruster.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRightThruster;
                @RightThruster.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRightThruster;
                @RightThruster.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRightThruster;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftThruster.started += instance.OnLeftThruster;
                @LeftThruster.performed += instance.OnLeftThruster;
                @LeftThruster.canceled += instance.OnLeftThruster;
                @RightThruster.started += instance.OnRightThruster;
                @RightThruster.performed += instance.OnRightThruster;
                @RightThruster.canceled += instance.OnRightThruster;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface IGameActions
    {
        void OnLeftThruster(InputAction.CallbackContext context);
        void OnRightThruster(InputAction.CallbackContext context);
    }
}
