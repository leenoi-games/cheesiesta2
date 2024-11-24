using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName= "Variable/Vector3")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] Vector3 value;
    [SerializeField] Vector3 defaultValue = new Vector3(0,0,0);

    public class VariableEvent : UnityEvent {}
    private VariableEvent m_OnValueChanged = new VariableEvent();
    
    public VariableEvent onValueChanged
    {
        get { return m_OnValueChanged; }
        set { m_OnValueChanged = value; }
    }

    public void SetValue(Vector3 f)
    {
        value = f;
        m_OnValueChanged.Invoke();
    }

    public Vector3 GetValue()
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
