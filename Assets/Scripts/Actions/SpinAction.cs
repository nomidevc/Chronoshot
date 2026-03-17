using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float m_rotationAmount;
    
    void Update()
    {
        if(!m_isActive) return;
        
        float rotationAmountPerFrame = 360f * Time.deltaTime; // Rotate 360 degrees per second
        transform.eulerAngles += new Vector3(0, rotationAmountPerFrame, 0);
        m_rotationAmount += rotationAmountPerFrame;
        if(m_rotationAmount >= 360f) 
        {
            StopSpin();
        }
    }
    
    public override void TakeAction(GridPosition gridPosition,Action onSpinComplete)
    {
        m_onActionComplete = onSpinComplete;
        m_isActive = true;
        m_rotationAmount = 0f;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition currentGridPosition = m_unit.GetGridPosition();
        return new List<GridPosition>() { currentGridPosition };
    }

    private void StopSpin()
    { 
        m_onActionComplete?.Invoke();
        m_isActive = false;
        m_rotationAmount = 0f;
    }

    public override string GetActionName()
    {
        return "Spin";
    }
}
