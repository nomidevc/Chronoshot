using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator _unitAnimator;
    [SerializeField] private int _maxMoveDistance = 1;
    
    private Unit m_unit;
    private Vector3 m_targetPosition;
    
    void Awake()
    {
        m_unit = GetComponent<Unit>();
        m_targetPosition = transform.position;
    }
    
    void Update()
    {
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(transform.position, m_targetPosition) > stoppingDistance)
        {
            _unitAnimator.SetBool("IsWalking", true);
            
            Vector3 moveDirection = (m_targetPosition - transform.position).normalized;
            float moveSpeed = 5f;
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            
            float rotationSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _unitAnimator.SetBool("IsWalking", false);
        }
    }
    
    public void SetTargetPosition(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
    }
    
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        
        GridPosition unitGridPosition = m_unit.GetGridPosition();
        
        for (int x = -_maxMoveDistance; x <= _maxMoveDistance ; x++)
        {
            for(int z = -_maxMoveDistance; z <= _maxMoveDistance ; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                
                Debug.Log(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }
}
