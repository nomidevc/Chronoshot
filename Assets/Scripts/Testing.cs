using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveAction moveAction = _unit.GetMoveAction();
            moveAction.GetValidActionGridPositionList();
        }
    }
}
