using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    [SerializeField] private int _maxShootDistance = 7;
    
    private float m_rotationAmount;
    
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        m_onActionComplete = onActionComplete;
        m_isActive = true;
        m_rotationAmount = 0f;
    }
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        
        GridPosition unitGridPosition = m_unit.GetGridPosition();

        for (int x = -_maxShootDistance; x <= _maxShootDistance; x++)
        {
            for (int z = -_maxShootDistance; z <= _maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                int maxDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (maxDistance > _maxShootDistance)
                {
                    continue;
                }
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                
                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                if (targetUnit.IsEnemy() == m_unit.IsEnemy()) continue;
                
                validGridPositionList.Add(testGridPosition);
                
            }
        }
        return validGridPositionList;
    }
        
    public override string GetActionName()
    {
        return "Shoot";
    }
}
