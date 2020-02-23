// GENERATED AUTOMATICALLY FROM 'Assets/Kinema2D/Kinema2D/Runtime/2DMoveAndCollide Core/Input/PlayerControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""229b82b6-87d2-4852-8ef2-a8fe7736963f"",
            ""actions"": [
                {
                    ""name"": ""South Button"",
                    ""type"": ""Button"",
                    ""id"": ""767b5463-6890-4b9f-8c3a-53399d935ec0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""East Button"",
                    ""type"": ""Button"",
                    ""id"": ""18f4181f-6e11-44d1-b97a-1a09f37c068d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""West Button"",
                    ""type"": ""Button"",
                    ""id"": ""1636a52c-7f7d-451a-b97f-c5513dcb0689"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""North Button"",
                    ""type"": ""Button"",
                    ""id"": ""5a706273-07ca-4ed4-8772-118c0c768d61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""d25016de-83f7-40eb-873c-4aaabf7cc3e9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""90a60013-8b97-4f7d-9ba4-1eed19e1c911"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""18fc2d44-a012-4307-be45-235e877978d6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""South Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f148cc71-9094-4a4c-84ce-9678be421aed"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""South Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff8a2aa9-04d8-4af4-8ea5-c7bccfff2d07"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01b9bc07-aedf-4834-8005-d12522f837e1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08ed0600-1f20-4286-b20c-1191b915fcb2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2ef1b98-376e-47c1-91ab-afeb6f967ce8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6c08dee-d5e2-4b54-b620-c0ab1b1be9f5"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""11e05455-7def-4717-9383-bffb67ba8feb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b28c89b8-f36e-45d5-86a7-826be6028474"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""642f83c9-3605-45ab-a1c2-338eccae5fc0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f7b374e3-6bf6-470e-8fd0-e260d5156c45"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d18d941-e358-40ac-a873-fa4669889017"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""72252762-0264-4360-8a2a-7006c2843043"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4548829e-f51e-4f60-993b-1635629ad113"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79489bf5-2a23-4dbf-8a90-e453772b938a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c52278d4-179d-4d8e-aa3f-8f5e87b59cb7"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa840bcd-4b55-402a-ba27-5cd2ed15d67b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""316a453e-5d8a-4210-bb81-31891923778a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7afda87b-e5d3-4235-9910-2348a0d0b4a5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a209a1d-560c-472e-873a-9fa71afc89bb"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_SouthButton = m_Player.GetAction("South Button");
        m_Player_EastButton = m_Player.GetAction("East Button");
        m_Player_WestButton = m_Player.GetAction("West Button");
        m_Player_NorthButton = m_Player.GetAction("North Button");
        m_Player_Horizontal = m_Player.GetAction("Horizontal");
        m_Player_RightTrigger = m_Player.GetAction("Right Trigger");
    }

    ~PlayerControls()
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_SouthButton;
    private readonly InputAction m_Player_EastButton;
    private readonly InputAction m_Player_WestButton;
    private readonly InputAction m_Player_NorthButton;
    private readonly InputAction m_Player_Horizontal;
    private readonly InputAction m_Player_RightTrigger;
    public struct PlayerActions
    {
        private PlayerControls m_Wrapper;
        public PlayerActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SouthButton => m_Wrapper.m_Player_SouthButton;
        public InputAction @EastButton => m_Wrapper.m_Player_EastButton;
        public InputAction @WestButton => m_Wrapper.m_Player_WestButton;
        public InputAction @NorthButton => m_Wrapper.m_Player_NorthButton;
        public InputAction @Horizontal => m_Wrapper.m_Player_Horizontal;
        public InputAction @RightTrigger => m_Wrapper.m_Player_RightTrigger;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                SouthButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSouthButton;
                SouthButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSouthButton;
                SouthButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSouthButton;
                EastButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEastButton;
                EastButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEastButton;
                EastButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEastButton;
                WestButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWestButton;
                WestButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWestButton;
                WestButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWestButton;
                NorthButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNorthButton;
                NorthButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNorthButton;
                NorthButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNorthButton;
                Horizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontal;
                Horizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontal;
                Horizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontal;
                RightTrigger.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightTrigger;
                RightTrigger.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightTrigger;
                RightTrigger.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightTrigger;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                SouthButton.started += instance.OnSouthButton;
                SouthButton.performed += instance.OnSouthButton;
                SouthButton.canceled += instance.OnSouthButton;
                EastButton.started += instance.OnEastButton;
                EastButton.performed += instance.OnEastButton;
                EastButton.canceled += instance.OnEastButton;
                WestButton.started += instance.OnWestButton;
                WestButton.performed += instance.OnWestButton;
                WestButton.canceled += instance.OnWestButton;
                NorthButton.started += instance.OnNorthButton;
                NorthButton.performed += instance.OnNorthButton;
                NorthButton.canceled += instance.OnNorthButton;
                Horizontal.started += instance.OnHorizontal;
                Horizontal.performed += instance.OnHorizontal;
                Horizontal.canceled += instance.OnHorizontal;
                RightTrigger.started += instance.OnRightTrigger;
                RightTrigger.performed += instance.OnRightTrigger;
                RightTrigger.canceled += instance.OnRightTrigger;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnSouthButton(InputAction.CallbackContext context);
        void OnEastButton(InputAction.CallbackContext context);
        void OnWestButton(InputAction.CallbackContext context);
        void OnNorthButton(InputAction.CallbackContext context);
        void OnHorizontal(InputAction.CallbackContext context);
        void OnRightTrigger(InputAction.CallbackContext context);
    }
}
