using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMapType
{
    Player,
    UI,
    MusicLevel,
    Dialogue,
    PointnClick
}

[CreateAssetMenu(fileName = "InputManager", menuName = "Managers/InputManager", order = 0)]
public class InputManager : ScriptableObject
{
    [SerializeField] InputActions playerInput;

    private void OnEnable() 
    {
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
        if(playerInput == null)
        {
            Debug.Log("No Player Input found");
            playerInput = new InputActions();
        }
        //gameStateEventListener?.OnEnableEvent(ChangeActionMap);
    }

    private void OnDisable()
    {
        //gameStateEventListener?.OnDisable();
    }

    
    public InputActions GetPlayerInput()
    {
        return playerInput;
    }
}