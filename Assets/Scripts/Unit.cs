using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition m_currentGridPosition;
    private MoveAction m_moveAction;
    

    void Start()
    {
        m_currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(m_currentGridPosition, this);
        
        m_moveAction = GetComponent<MoveAction>();
    }

    void Update()
    {
        UpdateUnitGridPosition();
    }
    
    
    private void UpdateUnitGridPosition()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != m_currentGridPosition)
        {
            LevelGrid.Instance.ChangeUnitGridPosition(this, m_currentGridPosition, newGridPosition);
            m_currentGridPosition = newGridPosition;
        }
    }
    
    public MoveAction GetMoveAction() => m_moveAction;
    
    public GridPosition GetGridPosition() => m_currentGridPosition;

    public override string ToString()
    {
        return $"Unit: {m_currentGridPosition.x} + {m_currentGridPosition.z}";
    }
}
