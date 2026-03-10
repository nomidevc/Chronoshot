using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit m_unit;
    protected bool m_isActive;
    
    protected virtual void Awake()
    {
        m_unit = GetComponent<Unit>();
    }
}
