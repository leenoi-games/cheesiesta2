using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CatMovement : MonoBehaviour
{
    [SerializeField] Transform m_target;
    private CatManager m_catManager;
    [SerializeField] float m_rotationSpeed = 360f;

    private NavMeshAgent m_agent;
    private Transform m_lastKnownPosition;
    private bool m_startedStroll = false;
    [SerializeField] float m_patrolTimeMin = 6;
    [SerializeField] float m_patrolTimeMax = 15;

     [SerializeField] float m_patrolDis = 6;
    private float m_patrolTimer = 0;

    private void Start()
    {
        m_catManager = GetComponent<CatManager>();
        m_agent = GetComponent<NavMeshAgent>();
        m_lastKnownPosition = transform;
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;
    }

    private void Update() 
    {
        if(m_catManager.IsChasing())
        {
            m_startedStroll = false;
            Chase();
        }
        else if(m_catManager.IsStrolling())
        {   
            Stroll();
            if(m_agent.remainingDistance < 1.0f)
            {
                Patrol();
            }
        }
        else
        {
            m_startedStroll = false;
            m_agent.SetDestination(transform.position);
        }
    }
    private void Patrol()
    {
        if(m_patrolTimer <= 0)
        {
            float newTimer = Random.Range(m_patrolTimeMin, m_patrolTimeMax);
            float x = Random.Range(-m_patrolDis,m_patrolDis);
            float y = Random.Range(-m_patrolDis,m_patrolDis);

            Vector3 newDestination = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            m_agent.SetDestination(newDestination);
        }
        m_patrolTimer -= Time.deltaTime;
    }

    private void Chase()
    {
        m_agent.speed = m_catManager.GetSpeed();
        m_agent.SetDestination(m_target.position);
        m_lastKnownPosition = m_target;

        var vel = m_agent.velocity;
        vel.z = 0;
        if (vel != Vector3.zero) 
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward,vel);
        }
    }

    private void Stroll()
    {
        if(m_startedStroll == false)
        {
            m_startedStroll = true;
            m_agent.SetDestination(m_lastKnownPosition.position);
            m_agent.speed = m_catManager.GetSpeed();
        }

        var vel = m_agent.velocity;
        vel.z = 0;
        if (vel != Vector3.zero) 
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward,vel);
        }
    }


}
