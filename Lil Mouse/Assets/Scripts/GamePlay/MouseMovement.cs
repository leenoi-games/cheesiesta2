using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class MouseMovement : NetworkBehaviour
{
    [Header("Chracteristics")]
    [SerializeField] private float m_rotationSpeed = 20f;
    private Vector3 m_moveAxis;
    private MouseManager m_mouseManager;

    Rigidbody2D m_rb;
    public override void OnNetworkSpawn()
    {
        if(!IsOwner) Destroy(this);
    }
    
    private void Start() 
    {
        m_moveAxis = new Vector3(0,0,0);
        m_rb = GetComponent<Rigidbody2D>();
        m_mouseManager = GetComponent<MouseManager>();
    }

    public void Move()
    {
        m_moveAxis.Normalize();
        float speed = m_mouseManager.GetMouseState().speed;
        m_rb.velocity = new Vector2(m_moveAxis.x * speed, m_moveAxis.y * speed);
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, m_moveAxis);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, m_rotationSpeed);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MoveOnEvent(Vector3 movePos)
    {
        m_moveAxis = movePos;
    }
}
