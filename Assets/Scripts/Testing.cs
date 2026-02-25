using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem m_gridSystem;
    void Start()
    {
        m_gridSystem = new GridSystem(10, 10, 2f);
    }

    void Update()
    {
        Debug.Log(m_gridSystem.GetGridPosition(MouseWorld.GetMouseWorldPosition()));
    }
}
