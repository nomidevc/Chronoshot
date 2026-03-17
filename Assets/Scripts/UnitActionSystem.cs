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
        
        if (Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;
            
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMouseWorldPosition());
            if (_selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                _selectedUnit.GetMoveAction().SetTargetPosition(mouseGridPosition, ClearBusy);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            _selectedUnit.GetSpinAction().StartSpin(ClearBusy);
        }
    }  
    
    private bool TryHandleUnitSelection()
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
        return false;
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
