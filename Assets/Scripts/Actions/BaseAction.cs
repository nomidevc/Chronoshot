using System;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit m_unit;
    protected bool m_isActive;
    protected Action m_onActionComplete;
    
    protected virtual void Awake()
    {
        m_unit = GetComponent<Unit>();
    }
    
    public abstract string GetActionName();
}
