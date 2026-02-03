using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance {get; private set;}
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction; 
    private PlayerInputAction PlayerInputActions;
    public event EventHandler OnTogglePausedGame;
    public event EventHandler OnBindingRebind;
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Intract,
        Intract_Alternate,
        Pause,
    }
    private void Awake()
    {
        PlayerInputActions = new PlayerInputAction();
        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            PlayerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += Interact_performed;
        PlayerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        PlayerInputActions.Player.Pause.performed += Pause_performed;
        Instance = this;
    }

    private void OnDestroy()
    {
        PlayerInputActions.Player.Interact.performed -= Interact_performed;
        PlayerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        PlayerInputActions.Player.Pause.performed -= Pause_performed;
        PlayerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnTogglePausedGame?.Invoke(this, EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
       
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }
        
    }

    public Vector2 GetInputVector()
    {
       Vector2 InputVector = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        return InputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return PlayerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return PlayerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Right:
                return PlayerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Left:
                return PlayerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Intract:
                return PlayerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Intract_Alternate:
                return PlayerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return PlayerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }
    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        InputAction inputAction;
        int BindingIndex;
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction =  PlayerInputActions.Player.Move;
                BindingIndex = 1;
                break;
             case Binding.Move_Down:
                inputAction =  PlayerInputActions.Player.Move;
                BindingIndex = 2;
                break;
             case Binding.Move_Right:
                inputAction =  PlayerInputActions.Player.Move;
                BindingIndex = 4;
                break;
             case Binding.Move_Left:
                inputAction =  PlayerInputActions.Player.Move;
                BindingIndex = 3;
                break;
             case Binding.Intract:
                inputAction =  PlayerInputActions.Player.Interact;
                BindingIndex = 0;
                break;
             case Binding.Intract_Alternate:
                inputAction =  PlayerInputActions.Player.InteractAlternate;
                BindingIndex = 0;
                break;
             case Binding.Pause:
                inputAction =  PlayerInputActions.Player.Pause;
                BindingIndex = 0;
                break;
        }
        PlayerInputActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(BindingIndex)
            .OnComplete(CallBack =>
            {
                CallBack.Dispose();
                PlayerInputActions.Player.Enable();
                OnActionRebound();
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, PlayerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();

    }
}
