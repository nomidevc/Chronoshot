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
        m_gridSystem.GetGridObject(gridPosition).SetUnit(unit);
    }
    
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        return m_gridSystem.GetGridObject(gridPosition).GetUnit();
    }
    
    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {
        m_gridSystem.GetGridObject(gridPosition).ClearUnit();
    }
    
    public void ChangeUnitGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        ClearUnitAtGridPosition(fromGridPosition);
        SetUnitAtGridPosition(toGridPosition, unit);
    }
    
    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        m_gridSystem.GetGridPosition(worldPosition);
    

}
