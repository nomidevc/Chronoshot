using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform _debugGridObject;
    
    private GridSystem m_gridSystem;
    
    void Start()
    {
        m_gridSystem = new GridSystem(10, 10, 2f);
        m_gridSystem.CreateDebugObject(_debugGridObject);
    }

    void Update()
    {
        Debug.Log(m_gridSystem.GetGridPosition(MouseWorld.GetMouseWorldPosition()));
    }
}
