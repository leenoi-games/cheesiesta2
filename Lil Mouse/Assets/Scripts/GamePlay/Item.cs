using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool m_playerinRange;
    [SerializeField] MouseManager m_mouse;

    private void Start() {
        m_playerinRange = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Press Space to Pick Up Cheese");
        if(other.gameObject.tag == "Player")
        {
            m_playerinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            m_playerinRange = false;
        }
    }

    public void OnCheesePickUp()
    {
        if(m_playerinRange)
        {
            m_mouse.PickUpItem(this);
        }
    }
}
