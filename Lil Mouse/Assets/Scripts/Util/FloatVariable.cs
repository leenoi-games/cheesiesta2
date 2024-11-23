using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName= "Variable/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] float value;
    [SerializeField] float defaultValue = 0f;

    public class VariableEvent : UnityEvent {}
    private VariableEvent m_OnValueChanged = new VariableEvent();
    
    public VariableEvent onValueChanged
    {
        get { return m_OnValueChanged; }
        set { m_OnValueChanged = value; }
    }

    public void SetValue(float f)
    {
        value = f;
        m_OnValueChanged.Invoke();
    }

    public float GetValue()
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
