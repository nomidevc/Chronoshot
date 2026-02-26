public class GridObject
{
    private GridSystem m_gridSystem;
    private GridPosition m_gridPosition;
    private Unit m_unit;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        m_gridPosition = gridPosition;
        m_gridSystem = gridSystem;
    }
    
    public void SetUnit(Unit unit)
    {
        m_unit = unit;
    }
    
    public Unit GetUnit()
    {
        return m_unit;
    }
    
    public void ClearUnit()
    {
        m_unit = null;
    }

    public override string ToString()
    {
        return m_gridPosition.ToString() + '\n'
            + m_unit;
    }
}
