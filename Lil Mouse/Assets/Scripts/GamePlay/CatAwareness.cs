using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AwarenessState
{
    Hearing,
    Seeing,
    HearOrSee,
    Silence,
    Kill,
    None
}

public class CatAwareness : MonoBehaviour
{
    [SerializeField] CatManager m_catManager;
    [SerializeField] AwarenessState m_awarenessState;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            m_catManager.MouseInRange(m_awarenessState);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_catManager.MouseOutOfRange(m_awarenessState);
        }
    }
}
