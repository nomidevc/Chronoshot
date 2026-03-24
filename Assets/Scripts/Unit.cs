using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition m_currentGridPosition;
    private BaseAction[] m_baseActionArray;
    private MoveAction m_moveAction;
    private SpinAction m_spinAction;
    private int m_actionPoint = 2;

    void Awake()
    {
        m_moveAction = GetComponent<MoveAction>();
        m_spinAction = GetComponent<SpinAction>();
        m_baseActionArray = GetComponents<BaseAction>();
    }


    void Start()
    {
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

    private void SpendActionPoints(BaseAction baseAction)
    {
        m_actionPoint -= baseAction.GetActionPointsCost();
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
    
    public MoveAction GetMoveAction() => m_moveAction;
    
    public SpinAction GetSpinAction() => m_spinAction;

    public int GetRemainingActionPoints() => m_actionPoint;
    
    public BaseAction[] GetBaseActionArray() => m_baseActionArray;
    
    public GridPosition GetGridPosition() => m_currentGridPosition;

    public override string ToString()
    {
        return $"Unit: {m_currentGridPosition.x} + {m_currentGridPosition.z}";
    }
}
