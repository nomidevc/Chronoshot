using System.Collections.Generic;
public class GridObject
{
    private GridSystem m_gridSystem;
    private GridPosition m_gridPosition;
    private List<Unit> m_unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        m_gridPosition = gridPosition;
        m_gridSystem = gridSystem;
        
        m_unitList = new List<Unit>();
    }
    
    public void AddUnit(Unit unit)
    {
        m_unitList.Add(unit);
    }

    public List<Unit> GetUnitList()
    {
        return m_unitList;
    }
    
    public void RemoveUnit(Unit unit)
    {
        m_unitList.Remove(unit);
    }
    
    public bool HasAnyUnit()
    {
        return m_unitList.Count > 0;
    }
    
    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return m_unitList[0];
        }
        else
        {
            return null;
        }
    }

    public override string ToString()
    {
        string unitListString = "";
        foreach (Unit unit in m_unitList)        {
            unitListString += unit.ToString() + '\n';
        }
        return m_gridPosition.ToString() + '\n'
            + unitListString;
    }
}
