using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAlarm : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private InputReader inputReader;

    // This is just so Unity loads my Managers
    void Start()
    {
        inputManager.hideFlags = HideFlags.DontUnloadUnusedAsset;
        inputReader.hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
