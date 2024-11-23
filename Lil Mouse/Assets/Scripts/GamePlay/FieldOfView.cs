using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] CatManager m_catManager;
    [SerializeField] float m_range;
    private bool hasUpdated = false;

    private void FixedUpdate() 
    {
        Vector3 playerDirection = m_target.position - transform.position ;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerDirection);
        if(ray.collider != null)
        {
            bool hasLineOfSight = ray.collider.CompareTag("Player");
            Debug.DrawRay(transform.position, playerDirection, hasLineOfSight? Color.green : Color.red);
            

            if(hasLineOfSight && !hasUpdated  && ray.distance < m_range)
            {
                m_catManager.MouseInRange(AwarenessState.Seeing);
                hasUpdated = true;
            }
            if(!hasLineOfSight && hasUpdated)
            {
                m_catManager.MouseOutOfRange(AwarenessState.Seeing);
                hasUpdated = false;
            }
        }        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, m_range);
    }
}
