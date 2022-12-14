//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/MVC/Input/MyInputSystem.inputactions
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

public partial class @MyInputSystem : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInputSystem"",
    ""maps"": [
        {
            ""name"": ""StandartInput"",
            ""id"": ""cca74bb0-c122-4ac4-a363-64197ab33444"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""3739a123-80ff-44bb-a824-9f72a78b2b71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""e6776c00-a749-47d2-9db0-f87d6412a309"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Laser"",
                    ""type"": ""Button"",
                    ""id"": ""2ce5ee80-4a03-4e47-a342-bbeaef157aa4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7ce9595-a168-4d8e-bfb3-8ac2890f900e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyStandartInput"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""282be485-a4d4-4c10-897c-0c5f7337b238"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyStandartInput"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f466f33-e83b-4992-b157-6fbb53c651f2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyStandartInput"",
                    ""action"": ""Laser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MyStandartInput"",
            ""bindingGroup"": ""MyStandartInput"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // StandartInput
        m_StandartInput = asset.FindActionMap("StandartInput", throwIfNotFound: true);
        m_StandartInput_Move = m_StandartInput.FindAction("Move", throwIfNotFound: true);
        m_StandartInput_Shoot = m_StandartInput.FindAction("Shoot", throwIfNotFound: true);
        m_StandartInput_Laser = m_StandartInput.FindAction("Laser", throwIfNotFound: true);
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

    // StandartInput
    private readonly InputActionMap m_StandartInput;
    private IStandartInputActions m_StandartInputActionsCallbackInterface;
    private readonly InputAction m_StandartInput_Move;
    private readonly InputAction m_StandartInput_Shoot;
    private readonly InputAction m_StandartInput_Laser;
    public struct StandartInputActions
    {
        private @MyInputSystem m_Wrapper;
        public StandartInputActions(@MyInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_StandartInput_Move;
        public InputAction @Shoot => m_Wrapper.m_StandartInput_Shoot;
        public InputAction @Laser => m_Wrapper.m_StandartInput_Laser;
        public InputActionMap Get() { return m_Wrapper.m_StandartInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandartInputActions set) { return set.Get(); }
        public void SetCallbacks(IStandartInputActions instance)
        {
            if (m_Wrapper.m_StandartInputActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnMove;
                @Shoot.started -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnShoot;
                @Laser.started -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnLaser;
                @Laser.performed -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnLaser;
                @Laser.canceled -= m_Wrapper.m_StandartInputActionsCallbackInterface.OnLaser;
            }
            m_Wrapper.m_StandartInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Laser.started += instance.OnLaser;
                @Laser.performed += instance.OnLaser;
                @Laser.canceled += instance.OnLaser;
            }
        }
    }
    public StandartInputActions @StandartInput => new StandartInputActions(this);
    private int m_MyStandartInputSchemeIndex = -1;
    public InputControlScheme MyStandartInputScheme
    {
        get
        {
            if (m_MyStandartInputSchemeIndex == -1) m_MyStandartInputSchemeIndex = asset.FindControlSchemeIndex("MyStandartInput");
            return asset.controlSchemes[m_MyStandartInputSchemeIndex];
        }
    }
    public interface IStandartInputActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnLaser(InputAction.CallbackContext context);
    }
}
