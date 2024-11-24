using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class PlayerNetwork : NetworkBehaviour
{

    private readonly NetworkVariable<PlayerNetworkData> m_netState = new(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 m_vel;
    private float m_rotationVel;
    [SerializeField] private float m_cheapInterpolationTime;

    private void Update() 
    {
        if(IsOwner)
        {
            m_netState.Value = new PlayerNetworkData
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles
            };
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_netState.Value.Position, ref m_vel, m_cheapInterpolationTime);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, m_netState.Value.Rotation.z, ref m_rotationVel, m_cheapInterpolationTime));
        }
    }

    struct PlayerNetworkData : INetworkSerializable
    {
        private float m_xPos;
        private float m_yPos;
        private short m_zRot;

        internal Vector3 Position 
        {
            get => new Vector3(m_xPos, m_yPos, 0);
            set 
            {
                m_xPos = value.x;
                m_yPos = value.y;
            }
        }

        internal Vector3 Rotation 
        {
            get => new Vector3(0, 0, m_zRot);
            set 
            {
                m_zRot = (short)value.z;
            }
        }

        /*
        Game Data:
        - Number of Lifes
        - Number of Collected cheeses

        Cat Data:
        - Pos
        - Rot
        - State

        Cheese Data:
        - Pos
        */

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref m_xPos);
            serializer.SerializeValue(ref m_yPos);
            serializer.SerializeValue(ref m_zRot);

        }
    }
}
