using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform _gridSystemVisualSinglePrefab;
    
    private GridSystemVisualSingle[,] m_gridSystemVisualSingleArray;

    void Start()
    {
        m_gridSystemVisualSingleArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetGridWidth(), LevelGrid.Instance.GetGridHeight()];
        
        for (int x = 0; x < LevelGrid.Instance.GetGridWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetGridHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(_gridSystemVisualSinglePrefab, 
                    LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                
                m_gridSystemVisualSingleArray[x, z] = 
                    gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    void Update()
    {
        UpdateGridVisual();
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();
        
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }

    private void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetGridWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetGridHeight(); z++)
            {
                m_gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }
    
    private void ShowGridPositionList(List<GridPosition> gridPositionList)
    { 
        foreach (GridPosition gridPosition in gridPositionList) 
        { 
            m_gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }
}
