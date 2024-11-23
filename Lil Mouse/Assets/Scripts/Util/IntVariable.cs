using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName= "Variable/Integer")]
public class IntVariable : ScriptableObject
{
    [SerializeField] int value;
    [SerializeField] int defaultValue = 0;
    public class VariableEvent : UnityEvent {}
    private VariableEvent m_OnValueChanged = new VariableEvent();
    
    public VariableEvent onValueChanged
    {
        get { return m_OnValueChanged; }
        set { m_OnValueChanged = value; }
    }

    public void SetValue(int f)
    {
        value = f;
        m_OnValueChanged.Invoke();
    }

    public void AddValue(int i)
    {
        SetValue(i + value);
    }

    public int GetValue()
    {
        return this.value;
    }
    public void SetDefault()
    {
        value = defaultValue;
    }

    private void OnEnable() {
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
        value = defaultValue;
    }
}
