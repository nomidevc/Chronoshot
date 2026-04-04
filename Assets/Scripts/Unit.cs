using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyActionPointsChanged;
    
    [SerializeField] private bool _unitIsEnemy;
    
    private const int ACTION_POINTS_MAX = 2;
    
    private GridPosition m_currentGridPosition;
    private HealthSystem m_healthSystem;
    private BaseAction[] m_baseActionArray;
    private MoveAction m_moveAction;
    private SpinAction m_spinAction;
    private int m_actionPoint = ACTION_POINTS_MAX;

    void Awake()
    {
        m_healthSystem = GetComponent<HealthSystem>();
        m_moveAction = GetComponent<MoveAction>();
        m_spinAction = GetComponent<SpinAction>();
        m_baseActionArray = GetComponents<BaseAction>();
    }


    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        m_healthSystem.OnDieAction += HealthSystem_OnDieAction;
        
        m_currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(m_currentGridPosition, this);
    }

    void Update()
    {
        UpdateUnitGridPosition();
    }
    
    
    private void UpdateUnitGridPosition()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != m_currentGridPosition)
        {
            LevelGrid.Instance.ChangeUnitGridPosition(this, m_currentGridPosition, newGridPosition);
            m_currentGridPosition = newGridPosition;
        }
    }
    
    private bool CanSpendActionPoints(BaseAction baseAction)
    {
        return m_actionPoint >= baseAction.GetActionPointsCost();
    }
    
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if ((!TurnSystem.Instance.IsPLayerTurn() && _unitIsEnemy) ||
            (TurnSystem.Instance.IsPLayerTurn() && !_unitIsEnemy))
        {
            ResetSpendActionPoints();
        }
    }
    
    private void HealthSystem_OnDieAction()
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(m_currentGridPosition, this);
        Destroy(gameObject);
    }

    private void SpendActionPoints(BaseAction baseAction)
    {
        m_actionPoint -= baseAction.GetActionPointsCost();
        
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ResetSpendActionPoints()
    {
        m_actionPoint = ACTION_POINTS_MAX;
        
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public bool TrySpendActionPoints(BaseAction baseAction)
    {
        if (CanSpendActionPoints(baseAction))
        {
            SpendActionPoints(baseAction);
            return true;
        }
        return false;
    }

    public void TakeDamage()
    {
        m_healthSystem.TakeDamage(40);
    }
    
    public MoveAction GetMoveAction() => m_moveAction;
    
    public SpinAction GetSpinAction() => m_spinAction;

    public int GetRemainingActionPoints() => m_actionPoint;
    
    public BaseAction[] GetBaseActionArray() => m_baseActionArray;
    
    public GridPosition GetGridPosition() => m_currentGridPosition;
    
    public bool IsEnemy() => _unitIsEnemy;

    public override string ToString()
    {
        return $"Unit: {m_currentGridPosition.x} + {m_currentGridPosition.z}";
    }
}
