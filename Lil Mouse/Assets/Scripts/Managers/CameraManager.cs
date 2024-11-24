using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class CameraManager : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    private Transform target;

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            NetworkObject player = NetworkManager.Singleton.LocalClient.PlayerObject;
            target = player.gameObject.transform;
            m_virtualCamera.m_LookAt = target.transform;
            m_virtualCamera.m_Follow = target.transform;
        }
        
    }
}
