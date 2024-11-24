using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;


public class MouseManager : NetworkBehaviour
{
    private MouseState m_CurrentMouseState;
    private MouseState m_lastMoveState;
    [SerializeField] GameObject m_graveStone;
    [SerializeField] MouseState m_idleState;
    [SerializeField] MouseState m_moveState;
    [SerializeField] MouseState m_cheeseState;
    [SerializeField] MouseState m_sneakyState;
    [SerializeField] Animator m_animator;
    [SerializeField] Vector3Variable m_spawnPosition;
    [SerializeField] GameEvent m_OnLoseEvent;

    [SerializeField] Item m_currentItem;
    [SerializeField] IntVariable m_lives;

    private void Start() 
    {
        m_CurrentMouseState = m_idleState;
        m_lastMoveState = m_moveState;
        transform.position = m_spawnPosition.GetValue();
    }

    public override void OnNetworkSpawn()
    {
        if(m_spawnPosition != null)
        {
            transform.position = m_spawnPosition.GetValue();
        }
        else
        {
            Debug.LogWarning("Player Spawn Position not set; Defaulting to (0,0,0).");
        }
        
        
        if(!IsOwner) Destroy(this);
    }

    public void UpdateMoveState(MouseState mouseState)
    {
        m_lastMoveState = mouseState;
        if(m_CurrentMouseState != m_idleState)
        {
            UpdateState(mouseState);
        }
    }

    public void UpdateState(MouseState mouseState)
    {
        m_CurrentMouseState = mouseState;
    }

    public MouseState GetMouseState()
    {
        return m_CurrentMouseState;
    }

    public void MoveOnEvent(Vector3 movePos)
    {
        if(movePos != Vector3.zero)
        {
            UpdateState(m_lastMoveState);
            m_animator.SetBool("idle",false);
        } 
        else
        {
            UpdateState(m_idleState);
            m_animator.SetBool("idle",true);
        }
    }

    public void PickUpItem(Item item)
    {
        //TODO: What happens if multiple Items in Range?? --> check for Range
        if(m_currentItem != null)
        {
            DropItem();
        }
        m_currentItem = item;
        item.gameObject.SetActive(false);
        m_animator.SetBool("cheese",true);
        UpdateMoveState(m_cheeseState);
    }

    public void OnDie()
    {
        Instantiate(m_graveStone, transform.position, Quaternion.identity);
        DropItem();
        transform.position = m_spawnPosition.GetValue();
        m_lives.AddValue(-1);
        if(m_lives.GetValue() <= 0)
        {
            gameObject.SetActive(false);
            m_OnLoseEvent.Raise();
        }
    }

    public bool CarriesCheese()
    {
        if(m_currentItem != null)
        {
            return true;
        }
        return false;
    }

    public void DropItem()
    {
        if(m_currentItem == null)
        {
            Debug.Log("WTF WERE YOU HOLDING? Dropping Empty Item");
            return;
        }
        m_currentItem.gameObject.transform.position = transform.position;
        m_animator.SetBool("cheese",false);
        m_currentItem.gameObject.SetActive(true);
        UpdateMoveState(m_moveState);
        m_currentItem = null;
    }
}
