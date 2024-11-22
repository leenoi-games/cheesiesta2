using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] IntVariable m_cheeseCounter;
    [SerializeField] MouseManager m_mouseManager;
    [SerializeField] List<GameObject> mice;

    private void Start() 
    {
        m_cheeseCounter.SetDefault();
    }

    public void OnDie()
    {
        if(mice != null)
        {
            GameObject firstMouse = mice[0];
            firstMouse.SetActive(false);
            mice.Remove(firstMouse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(m_mouseManager.CarriesCheese())
            {
                m_cheeseCounter.AddValue(1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(m_mouseManager.CarriesCheese())
            {
                m_cheeseCounter.AddValue(-1);
            }
        }
    }
}
