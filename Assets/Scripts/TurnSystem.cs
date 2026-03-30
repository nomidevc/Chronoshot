using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    
    public event EventHandler OnTurnChanged;
        
    private bool m_isPlayerTurn = true;
    private int m_turnCount = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void UpdateTurnCount()
    {
        m_turnCount++;
    }
    
    public void NextTurn()
    {
        UpdateTurnCount();
        m_isPlayerTurn = !m_isPlayerTurn;
        
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public int GetTurnCount() => m_turnCount;
    
    public bool IsPLayerTurn() => m_isPlayerTurn;
}
