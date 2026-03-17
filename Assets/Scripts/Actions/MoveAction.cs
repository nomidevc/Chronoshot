using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator _unitAnimator;
    [SerializeField] private int _maxMoveDistance = 1;
    
    private Vector3 m_targetPosition;
    
    protected override void Awake()
    {
        base.Awake();
        m_targetPosition = transform.position;
    }
    
    void Update()
    {
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
        if (!m_isActive) return;
        
        Vector3 moveDirection = (m_targetPosition - transform.position).normalized;
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(transform.position, m_targetPosition) > stoppingDistance)
        {
            _unitAnimator.SetBool("IsWalking", true);
            float moveSpeed = 5f;
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            
        }
        else
        {
            m_onActionComplete?.Invoke();
            _unitAnimator.SetBool("IsWalking", false);
            m_isActive = false;
        }
        
        float rotationSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }
    
    public override void TakeAction(GridPosition gridPosition, Action onMoveComplete)
    {
        m_onActionComplete = onMoveComplete;
        m_targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        m_isActive = true;
    }
    
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        
        GridPosition unitGridPosition = m_unit.GetGridPosition();
        
        for (int x = -_maxMoveDistance; x <= _maxMoveDistance ; x++)
        {
            for(int z = -_maxMoveDistance; z <= _maxMoveDistance ; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {                    
                    continue;
                }
                
                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return  "Move";
    }
}
