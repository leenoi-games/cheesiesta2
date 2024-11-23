using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatState", menuName = "GamePlay/CatState", order = 0)]
public class CatState : ScriptableObject
{
    [SerializeField] float m_speed;
    [SerializeField] float m_nextStateTimer;
    [SerializeField] float m_previousStateTimer;
    [SerializeField] CatState m_nextState;
    [SerializeField] CatState m_previousState;

    [SerializeField] AwarenessState m_nextCondition;
    [SerializeField] AwarenessState m_previousCondition;

    [SerializeField] string m_animator_variable;
    [SerializeField] bool m_animator_variable_state;


    public float GetNextStateTimer()
    {
        return m_nextStateTimer;
    }

    public float GetPreviousStateTimer()
    {
        return m_previousStateTimer;
    }

    public CatState GetPreviousState()
    {
        return m_previousState;
    }

    public CatState GetNextState()
    {
        return m_nextState;
    }

    public AwarenessState GetNextCondition()
    {
        return m_nextCondition;
    }

    public AwarenessState GetPreviousCondition()
    {
        return m_previousCondition;
    }

    public float GetSpeed()
    {
        return m_speed;
    }
    public string GetAnimatorVariable()
    {
        return m_animator_variable;
    }
    public bool GetAnimatorState()
    {
        return m_animator_variable_state;
    }
}
