using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction; 
    private PlayerInputAction PlayerInputActions;
    private void Awake()
    {
        PlayerInputActions = new PlayerInputAction();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += Interact_performed;
        PlayerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

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
