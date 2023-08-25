//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Input/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""dc5a34d6-07bd-402f-a46f-a84963cfaca7"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1b563eff-b63d-4b13-8460-3384f1aea5c8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e70d8e8b-9b02-4ecd-9f93-853367a9d4c5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d214460-fe8a-4c65-a2f3-9746c710602f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1093df29-dc2b-45f0-b012-134f07e594b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 3"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e2f39e40-ed15-43dd-adc0-5daddf1c4873"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 4"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4b77c418-336b-4feb-9c58-abd5e88f1830"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 5"",
                    ""type"": ""PassThrough"",
                    ""id"": ""077b89ca-4833-444c-a01f-d490769b1be6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 6"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4f473ce1-5c39-42c3-a858-5326a1b82ea0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 7"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7b471c9a-6de2-4038-8b4c-117c4af7e262"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 8"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c47ef8e1-cc6e-4cf3-ada6-6ef8543e3e76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 9"",
                    ""type"": ""PassThrough"",
                    ""id"": ""21d82963-d7dc-4424-9437-cb231a473e65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hotbar 10"",
                    ""type"": ""PassThrough"",
                    ""id"": ""62629b15-9640-4473-8df6-0f1138be6497"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse Wheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""22443041-2b7d-43f9-b5bf-ea91af458656"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Use Item"",
                    ""type"": ""PassThrough"",
                    ""id"": ""15e8e587-9ab7-45f5-b4eb-e01e27ee585a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""492771e3-8a8a-43a0-a351-89250f5809c0"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4d08b28-a3e1-4460-a698-be7781b1e337"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67c2eb1e-fe11-4878-875d-603c18f1c205"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""479d677e-c7e7-452f-8867-00d1c36f9fa3"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""507e4a9a-3400-42b2-a07e-9718164882f6"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a536ab47-ed93-4068-8c50-9a6a61ed4353"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4601ec75-522f-4482-88c6-07552beafadd"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f10b317d-1874-4228-8044-e93f1028af4d"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34c6ad23-7ded-4ebe-a02e-9cfa3bfd11a7"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87d0b2a2-cff2-41fb-9d0c-d715950df6c4"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hotbar 10"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fc22d3e-6103-4103-866c-abc0f6f7c1f3"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Wheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05d92ba9-18a2-4227-8c1d-74a4aca56702"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""201ed2bd-8fa6-4b88-bdeb-0c4f6c35afdb"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Right Stick"",
                    ""id"": ""6496a15f-faa8-46ef-94ef-2f25d6ec533b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""eae331b5-ad84-4efe-98dc-4cde1520f033"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a55f0f0d-0db2-44eb-bf16-bb35bb43f513"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""171e7a61-00d5-4159-8afa-86d2f06b89ea"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d95fd033-b081-42e3-8bfb-71ab6889cf1a"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""0c75a016-0f50-4f8d-ac0b-d007893750f2"",
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
                    ""id"": ""f7638d9a-6daf-432c-909c-deffc9a04b5b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f4053162-9bd2-4aec-95c5-dc50c3260f96"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0fbf7289-883e-403a-b806-482632501698"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3392959f-11ff-427d-b5c2-88212eefc0a5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""1bc7ac41-3a04-4f68-ae21-de650fb84ef0"",
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
                    ""id"": ""1d2bc3e8-142d-41c3-a8c8-614c7d4250c5"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fcc32b37-a6ef-47a3-8142-9d6f4d2be2f8"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fa760e69-d1e4-41a9-b439-06fdc50f4932"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5290fde3-33a4-4b4e-8cb2-1cc3df85db35"",
                    ""path"": ""<Gamepad>/leftStick/right"",
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
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Camera = m_Player.FindAction("Camera", throwIfNotFound: true);
        m_Player_Hotbar1 = m_Player.FindAction("Hotbar 1", throwIfNotFound: true);
        m_Player_Hotbar2 = m_Player.FindAction("Hotbar 2", throwIfNotFound: true);
        m_Player_Hotbar3 = m_Player.FindAction("Hotbar 3", throwIfNotFound: true);
        m_Player_Hotbar4 = m_Player.FindAction("Hotbar 4", throwIfNotFound: true);
        m_Player_Hotbar5 = m_Player.FindAction("Hotbar 5", throwIfNotFound: true);
        m_Player_Hotbar6 = m_Player.FindAction("Hotbar 6", throwIfNotFound: true);
        m_Player_Hotbar7 = m_Player.FindAction("Hotbar 7", throwIfNotFound: true);
        m_Player_Hotbar8 = m_Player.FindAction("Hotbar 8", throwIfNotFound: true);
        m_Player_Hotbar9 = m_Player.FindAction("Hotbar 9", throwIfNotFound: true);
        m_Player_Hotbar10 = m_Player.FindAction("Hotbar 10", throwIfNotFound: true);
        m_Player_MouseWheel = m_Player.FindAction("Mouse Wheel", throwIfNotFound: true);
        m_Player_UseItem = m_Player.FindAction("Use Item", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Camera;
    private readonly InputAction m_Player_Hotbar1;
    private readonly InputAction m_Player_Hotbar2;
    private readonly InputAction m_Player_Hotbar3;
    private readonly InputAction m_Player_Hotbar4;
    private readonly InputAction m_Player_Hotbar5;
    private readonly InputAction m_Player_Hotbar6;
    private readonly InputAction m_Player_Hotbar7;
    private readonly InputAction m_Player_Hotbar8;
    private readonly InputAction m_Player_Hotbar9;
    private readonly InputAction m_Player_Hotbar10;
    private readonly InputAction m_Player_MouseWheel;
    private readonly InputAction m_Player_UseItem;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Camera => m_Wrapper.m_Player_Camera;
        public InputAction @Hotbar1 => m_Wrapper.m_Player_Hotbar1;
        public InputAction @Hotbar2 => m_Wrapper.m_Player_Hotbar2;
        public InputAction @Hotbar3 => m_Wrapper.m_Player_Hotbar3;
        public InputAction @Hotbar4 => m_Wrapper.m_Player_Hotbar4;
        public InputAction @Hotbar5 => m_Wrapper.m_Player_Hotbar5;
        public InputAction @Hotbar6 => m_Wrapper.m_Player_Hotbar6;
        public InputAction @Hotbar7 => m_Wrapper.m_Player_Hotbar7;
        public InputAction @Hotbar8 => m_Wrapper.m_Player_Hotbar8;
        public InputAction @Hotbar9 => m_Wrapper.m_Player_Hotbar9;
        public InputAction @Hotbar10 => m_Wrapper.m_Player_Hotbar10;
        public InputAction @MouseWheel => m_Wrapper.m_Player_MouseWheel;
        public InputAction @UseItem => m_Wrapper.m_Player_UseItem;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera;
                @Hotbar1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar1;
                @Hotbar1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar1;
                @Hotbar1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar1;
                @Hotbar2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar2;
                @Hotbar2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar2;
                @Hotbar2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar2;
                @Hotbar3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar3;
                @Hotbar3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar3;
                @Hotbar3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar3;
                @Hotbar4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar4;
                @Hotbar4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar4;
                @Hotbar4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar4;
                @Hotbar5.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar5;
                @Hotbar5.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar5;
                @Hotbar5.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar5;
                @Hotbar6.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar6;
                @Hotbar6.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar6;
                @Hotbar6.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar6;
                @Hotbar7.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar7;
                @Hotbar7.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar7;
                @Hotbar7.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar7;
                @Hotbar8.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar8;
                @Hotbar8.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar8;
                @Hotbar8.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar8;
                @Hotbar9.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar9;
                @Hotbar9.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar9;
                @Hotbar9.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar9;
                @Hotbar10.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar10;
                @Hotbar10.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar10;
                @Hotbar10.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHotbar10;
                @MouseWheel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseWheel;
                @UseItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Hotbar1.started += instance.OnHotbar1;
                @Hotbar1.performed += instance.OnHotbar1;
                @Hotbar1.canceled += instance.OnHotbar1;
                @Hotbar2.started += instance.OnHotbar2;
                @Hotbar2.performed += instance.OnHotbar2;
                @Hotbar2.canceled += instance.OnHotbar2;
                @Hotbar3.started += instance.OnHotbar3;
                @Hotbar3.performed += instance.OnHotbar3;
                @Hotbar3.canceled += instance.OnHotbar3;
                @Hotbar4.started += instance.OnHotbar4;
                @Hotbar4.performed += instance.OnHotbar4;
                @Hotbar4.canceled += instance.OnHotbar4;
                @Hotbar5.started += instance.OnHotbar5;
                @Hotbar5.performed += instance.OnHotbar5;
                @Hotbar5.canceled += instance.OnHotbar5;
                @Hotbar6.started += instance.OnHotbar6;
                @Hotbar6.performed += instance.OnHotbar6;
                @Hotbar6.canceled += instance.OnHotbar6;
                @Hotbar7.started += instance.OnHotbar7;
                @Hotbar7.performed += instance.OnHotbar7;
                @Hotbar7.canceled += instance.OnHotbar7;
                @Hotbar8.started += instance.OnHotbar8;
                @Hotbar8.performed += instance.OnHotbar8;
                @Hotbar8.canceled += instance.OnHotbar8;
                @Hotbar9.started += instance.OnHotbar9;
                @Hotbar9.performed += instance.OnHotbar9;
                @Hotbar9.canceled += instance.OnHotbar9;
                @Hotbar10.started += instance.OnHotbar10;
                @Hotbar10.performed += instance.OnHotbar10;
                @Hotbar10.canceled += instance.OnHotbar10;
                @MouseWheel.started += instance.OnMouseWheel;
                @MouseWheel.performed += instance.OnMouseWheel;
                @MouseWheel.canceled += instance.OnMouseWheel;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnHotbar1(InputAction.CallbackContext context);
        void OnHotbar2(InputAction.CallbackContext context);
        void OnHotbar3(InputAction.CallbackContext context);
        void OnHotbar4(InputAction.CallbackContext context);
        void OnHotbar5(InputAction.CallbackContext context);
        void OnHotbar6(InputAction.CallbackContext context);
        void OnHotbar7(InputAction.CallbackContext context);
        void OnHotbar8(InputAction.CallbackContext context);
        void OnHotbar9(InputAction.CallbackContext context);
        void OnHotbar10(InputAction.CallbackContext context);
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
    }
}
