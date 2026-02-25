using System;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro _gridPositionText;
    
    private GridObject m_gridObject;

    void Update()
    {
        SetGridInformation(m_gridObject);
    }

    private void SetGridInformation(GridObject gridObject)
    {
        _gridPositionText.text = gridObject.ToString();
    }
    
    public void SetGridObject(GridObject gridObject)
    {
        m_gridObject = gridObject;
    }
}
