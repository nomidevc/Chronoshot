using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    
    [SerializeField] private Transform _debugGridObject;
    
    private GridSystem m_gridSystem;
    
    void Awake()
    {   
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        m_gridSystem = new GridSystem(10, 10, 2f);
        m_gridSystem.CreateDebugObject(_debugGridObject);
    }
    
    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        m_gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }
    
    public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
    {
        return m_gridSystem.GetGridObject(gridPosition).GetUnitList();
    }
    
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        m_gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }
    
    public void ChangeUnitGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        SetUnitAtGridPosition(toGridPosition, unit);
    }
    
    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        m_gridSystem.GetGridPosition(worldPosition);
    
    public Vector3 GetWorldPosition(GridPosition gridPosition) =>
        m_gridSystem.GetWorldPosition(gridPosition);
    
    public int GetGridWidth() => m_gridSystem.GetGridWidth();
    
    public int GetGridHeight() => m_gridSystem.GetGridHeight();

    public bool IsValidGridPosition(GridPosition gridPosition) =>
        m_gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition) =>
        m_gridSystem.GetGridObject(gridPosition).HasAnyUnit();
    
}
