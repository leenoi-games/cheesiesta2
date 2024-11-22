using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] MouseManager m_mouseManager;

    [SerializeField] GameEvent m_killEvent;


    [Header("CatStates")]
    [SerializeField] CatState m_currentState;
    [SerializeField] CatState m_sleepState;
    [SerializeField] CatState m_alertState;
    [SerializeField] CatState m_chaseState;
    [SerializeField] CatState m_strollState;
    
    [Header("CatState Timer")]
    [SerializeField] float m_hearTimer = 0f;
    [SerializeField] float m_silenceTimer = 0f;
    [SerializeField] float m_seeTimer = 0f;
    [SerializeField] float m_blindTimer = 0f;

    private bool m_inHearingRange;
    private bool m_inSeeingRange;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = m_sleepState;
        m_inHearingRange = false;
    }

    private void ResetCat()
    {
        UpdateState(m_strollState);
        ResetTimer();
    }

    private void Update()
    {
        UpdateTimer();
        if(m_currentState.GetNextCondition() != AwarenessState.None && m_currentState.GetNextState() != null)
        {
            CheckNextCondition(m_currentState.GetNextCondition(), m_currentState.GetNextState(),m_currentState.GetNextStateTimer());
        }
        if(m_currentState.GetPreviousCondition() != AwarenessState.None && m_currentState.GetPreviousState() != null)
        {
            CheckNextCondition(m_currentState.GetPreviousCondition(), m_currentState.GetPreviousState(),m_currentState.GetPreviousStateTimer());
        }
    }

    private void CheckNextCondition(AwarenessState condition, CatState state, float timer)
    {
        //IK this is ugly but got no time
        if(condition == AwarenessState.Silence && m_silenceTimer > timer && m_blindTimer > timer)
        {
            UpdateState(state);
        }
        if(condition == AwarenessState.Hearing && m_hearTimer > timer)
        {
            UpdateState(state);
        }
        if(condition == AwarenessState.Seeing && m_seeTimer > timer)
        {
            UpdateState(state);
        }
        if(condition == AwarenessState.HearOrSee && (m_seeTimer > timer || m_hearTimer > timer))
        {
            UpdateState(state);
        }
    }

    private void UpdateTimer()
    {
        if(m_inHearingRange && m_mouseManager.GetMouseState().isLoud)
        {
            m_hearTimer += Time.deltaTime;
        } 
        else
        {
            m_silenceTimer+= Time.deltaTime;
        }
        if(m_inSeeingRange)
        {
            m_seeTimer += Time.deltaTime;
        }
        else
        {
            m_blindTimer += Time.deltaTime;
        }
    }

    
    public void UpdateState(CatState catState)
    {
        //Change Animation
        m_animator.SetBool(m_currentState.GetAnimatorVariable(), !m_currentState.GetAnimatorState());
        m_currentState = catState;
        ResetTimer();
        m_animator.SetBool(m_currentState.GetAnimatorVariable(), m_currentState.GetAnimatorState());
    }

    private void ResetTimer()
    {
        m_hearTimer = 0.0f;
        m_seeTimer = 0.0f;
        m_silenceTimer = 0.0f;
        m_blindTimer = 0.0f;
    }

    public void MouseInRange(AwarenessState awarenessState)
    {
        if(awarenessState == AwarenessState.Hearing)
        {
            if(m_mouseManager.GetMouseState().isLoud)
            {
                m_silenceTimer = 0.0f;
            } 
            //Debug.Log("Cat hears Mouse");
            m_inHearingRange = true;
        }
        else if(awarenessState == AwarenessState.Seeing)
        {
            //Debug.Log("Cat sees Mouse");
            m_inSeeingRange = true;
            m_blindTimer = 0.0f;
        }
        else if(awarenessState == AwarenessState.Kill && m_currentState == m_chaseState)
        {
            m_killEvent.Raise();
            ResetCat();
            m_mouseManager.OnDie();
        }
    }

    public void MouseOutOfRange(AwarenessState awarenessState)
    {
        if(awarenessState == AwarenessState.Hearing)
        {
            //Debug.Log("Cat stopped hearing Mouse");
            m_hearTimer = 0.0f;
            m_inHearingRange = false;
        }
        else if(awarenessState == AwarenessState.Seeing)
        {
            //Debug.Log("Cat stopped seeing Mouse");
            m_seeTimer = 0.0f;
            m_inSeeingRange = false;
        }
    }

    public bool IsChasing()
    {
        if(m_currentState == m_chaseState)
        {
            return true;
        }
        return false;
    }
    public bool IsStrolling()
    {
        if(m_currentState == m_strollState)
        {
            return true;
        }
        return false;
    }

    public float GetSpeed()
    {
        return m_currentState.GetSpeed();
    }

    public CatState GetCatState()
    {
        return m_currentState;
    }
}
