using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;
    
    protected Unit m_unit;
    protected bool m_isActive;
    protected Action m_onActionComplete;
    
    protected virtual void Awake()
    {
        m_unit = GetComponent<Unit>();
    }

    protected void ActionStart(Action onActionComplete)
    {
        m_isActive = true;
        m_onActionComplete = onActionComplete;
        
        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        m_isActive = false;
        m_onActionComplete?.Invoke();
        
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }
    
    public abstract string GetActionName();
    
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
    
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    
    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public Unit GetUnit() => m_unit;
}
