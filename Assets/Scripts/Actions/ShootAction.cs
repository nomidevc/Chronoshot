using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    public Action OnUnitShooting;
    
    [SerializeField] private int _maxShootDistance = 7;
    
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff
    }
    
    private float m_stateTimer;
    private State m_currentState;
    private Unit m_targetUnit;
    private bool canShoot = true;
    
    void Update()
    {
        if(!m_isActive) return;

        m_stateTimer -= Time.deltaTime;

        switch (m_currentState)
        {
            case State.Aiming:
                RotateToTheTarget();
                break;
            case State.Shooting:
                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                }
                break;
            case State.Cooloff:
                break;
        }
        
        if (m_stateTimer < 0f)
        {
            NextState();
        }
    }

    private void RotateToTheTarget()
    {
        float rotationSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, 
            (m_targetUnit.transform.position - transform.position).normalized, 
            rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        OnUnitShooting?.Invoke();
        m_targetUnit.TakeDamage();
    }
    
    private void NextState()
    {
        switch (m_currentState)     
        {
            case State.Aiming:
                m_currentState = State.Shooting;
                float shootingStateTime = 0.5f;
                m_stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                m_currentState = State.Cooloff;
                float coolOffStateTime = 0.1f;
                m_stateTimer = coolOffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        
        m_currentState = State.Aiming;
        float aimingStateTime = 1f;
        m_stateTimer = aimingStateTime;
        m_targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
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
