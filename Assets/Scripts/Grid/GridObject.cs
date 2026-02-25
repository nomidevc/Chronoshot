public class GridObject
{
    private GridSystem m_gridSystem;
    private GridPosition m_gridPosition;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        m_gridPosition = gridPosition;
        m_gridSystem = gridSystem;
    }

    public override string ToString()
    {
        return m_gridPosition.ToString();
    }
}
