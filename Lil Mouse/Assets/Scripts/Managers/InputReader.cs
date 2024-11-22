using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Managers/InputReader", order = 0)]
public class InputReader : ScriptableObject, InputActions.IMouseMovementActions
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private InputActions playerInput;

    [Header("GameEvents for Player Input")]
    [SerializeField] private VectorEvent moveEvent;
    [SerializeField] private GameEvent pickUpEvent;
    [SerializeField] private GameEvent dropEvent;
    
    private void OnEnable() 
    {
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
        SetCallbacks();
    }

    private void SetCallbacks()
    {
        /*if(inputManager != null)
        {
            playerInput = inputManager.GetPlayerInput();
            Debug.Log("Inputmanager Null");
        }
        else
        {
            playerInput = new InputActions();
            
        }*/
        
        playerInput = new InputActions();
        playerInput.MouseMovement.SetCallbacks(this);
        playerInput.Enable();
    }

    private void OnDisable() 
    {
        //playerInputInstantiatedListener?.OnDisable();
    }



//-------------------------------------------------------------
// ADVENTURE ACTIONS
//-------------------------------------------------------------

    public void OnWalk(InputAction.CallbackContext context)
    {
        if(moveEvent != null)
        {
            moveEvent.testProperty = context.ReadValue<Vector2>();
            moveEvent.Raise(context.ReadValue<Vector2>());
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if(pickUpEvent != null && context.phase == InputActionPhase.Performed)
        {
            pickUpEvent.Raise();
        }

        if(dropEvent != null && context.phase == InputActionPhase.Canceled)
        {
            dropEvent.Raise();
        }
    }
}

