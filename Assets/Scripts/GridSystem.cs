using UnityEngine;

public class GridSystem 
{
    private int m_gridWidth;
    private int m_gridHeight;
    private float m_cellSize;
    
    public GridSystem(int gridWidth, int gridHeight, float cellSize)
    {
        m_gridWidth = gridWidth;
        m_gridHeight = gridHeight;
        m_cellSize = cellSize;
        
        DrawGridLines();
    }

    private void DrawGridLines()
    {
        for (int x = 0; x < m_gridWidth; x++)
        {
            for (int z = 0; z < m_gridHeight; z++)
            {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + new Vector3(0.2f, 0, 0.2f)
                    , Color.white, 1000);
            }
        }
    }
    
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * m_cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.FloorToInt(worldPosition.x / m_cellSize), 
            Mathf.FloorToInt(worldPosition.z/ m_cellSize));
    }
    
    
}
