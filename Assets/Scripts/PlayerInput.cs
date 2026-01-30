using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance {get; private set;}
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction; 
    private PlayerInputAction PlayerInputActions;
    public event EventHandler OnTogglePausedGame;
    private void Awake()
    {
        PlayerInputActions = new PlayerInputAction();
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
}
