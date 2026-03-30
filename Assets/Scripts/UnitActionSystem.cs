using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler OnActionStarted;
    public event EventHandler<bool> OnBusyChanged;
    
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

        if (!TurnSystem.Instance.IsPLayerTurn()) return;
        
        if(EventSystem.current.IsPointerOverGameObject()) return;
        
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
                    if(unit == _selectedUnit) return false; // Already selected this unit
                    if (unit.IsEnemy()) return false; // Unit is enemy
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
            if(!m_selectedAction.IsValidActionGridPosition(mouseGridPosition)) return;
            if(!_selectedUnit.TrySpendActionPoints(m_selectedAction)) return;
            
            SetBusy();
            m_selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            OnActionStarted?.Invoke(this, EventArgs.Empty);
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
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ClearBusy()
    {
        OnBusyChanged?.Invoke(this, false);
        m_isBusy = false;
    }

    private void SetBusy()
    {
        OnBusyChanged?.Invoke(this, true);
        m_isBusy = true;
    }
    
    public Unit GetSelectedUnit() => _selectedUnit;
    
    public BaseAction GetSelectedAction() => m_selectedAction;
    
}
