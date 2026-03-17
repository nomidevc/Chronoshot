using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    
    [SerializeField] private Unit _selectedUnit;
    [SerializeField] private LayerMask _unitLayerMask;
    
    private BaseAction m_selectedAction;
    private bool m_isBusy;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        SetSelectedUnit(_selectedUnit);
    }

    void Update()
    {
        if (m_isBusy) return;
        
        if(TryHandleUnitSelection()) return;
        
        HandleActionSelection();
    }  
    
    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _unitLayerMask))
            { 
                if (raycastHit.transform.TryGetComponent(out Unit unit)) 
                { 
                    SetSelectedUnit(unit); 
                    return true;
                }
            }
        }
        return false;
    }
    
    private void HandleActionSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMouseWorldPosition());
            switch (m_selectedAction)
            {
                case MoveAction moveAction:
                    if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                    {
                        SetBusy();
                        moveAction.SetTargetPosition(mouseGridPosition, ClearBusy);
                    }
                    break;
                case SpinAction spinAction:
                    SetBusy();
                    spinAction.StartSpin(ClearBusy);
                    break;
            }
        }
    }

    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        
        SetSelectedAction(unit.GetMoveAction());
        
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void SetSelectedAction(BaseAction baseAction)
    {
        m_selectedAction = baseAction;
    }
    
    private void ClearBusy() => m_isBusy = false;
        
    private void SetBusy() => m_isBusy = true;
    
    public Unit GetSelectedUnit() => _selectedUnit;
    
}
