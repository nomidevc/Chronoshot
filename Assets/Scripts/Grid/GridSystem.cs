using UnityEngine;

public class GridSystem 
{
    private int m_gridWidth;
    private int m_gridHeight;
    private float m_cellSize;
    private GridObject[,] m_gridObjects;
    
    public GridSystem(int gridWidth, int gridHeight, float cellSize)
    {
        m_gridWidth = gridWidth;
        m_gridHeight = gridHeight;
        m_cellSize = cellSize;
        m_gridObjects = new GridObject[m_gridWidth, m_gridHeight];
        
        SetGridObjectsArray();
    }

    private void SetGridObjectsArray()
    {
        for (int x = 0; x < m_gridWidth; x++)
        {
            for (int z = 0; z < m_gridHeight; z++)
            {
                m_gridObjects[x, z] = new GridObject(this, new GridPosition(x, z));
            }
        }
    }
    
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * m_cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / m_cellSize), 
            Mathf.RoundToInt(worldPosition.z/ m_cellSize));
    }
    

    public void CreateDebugObject(Transform debugGridObject)
    {
        for (int x = 0; x < m_gridWidth; x++)
        {
            for (int z = 0; z < m_gridHeight; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                
                Transform debugTransform = GameObject.Instantiate(debugGridObject, 
                    GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }
    
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return m_gridObjects[gridPosition.x, gridPosition.z];
    }
    
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition is { x: >= 0, z: >= 0 }
            && gridPosition.x < m_gridWidth && gridPosition.z < m_gridHeight;
    }

}
