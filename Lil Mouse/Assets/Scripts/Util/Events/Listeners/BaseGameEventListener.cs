using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
    IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    Type typeParameterType = typeof(E);
    public E Event;

    public E gameEvent { get { return Event; } set { Event = value; } }
    public float delay;
    [Tooltip("Response to invoke when Event is raised.")]
    public UER Response;
    [Space(15)] public bool invokeCheckOnEnable = false;
    public bool m_disableFirstEvent = false;
    bool runEvent = true;

    private void Start()
    {
        if (m_disableFirstEvent)
            runEvent = false;
    }
    private void OnEnable()
    {
        if (Event)
            Event.RegisterListener(this);
        if (invokeCheckOnEnable)
        {
            if (gameEvent)
                OnEventRaised(gameEvent.m_previousValue);
        }
    }
    
    private void OnDisable()
    {
        if (Event)
            Event.UnregisterListener(this);
    }
    public void OnEventRaised(T item)
    {
        if (!runEvent)
        {
            if (m_disableFirstEvent)
                runEvent = true;
            return;
        }
        if (Event)
        {
            Response?.Invoke(item);
        }

    }
    public static Type GetListType<U>()
    {
        return typeof(U);
    }
}