using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float m_timer;

    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        m_timer = 2f;
    }

    void Update()
    {
        if (TurnSystem.Instance.IsPLayerTurn()) return;
        
        m_timer -= Time.deltaTime;
        if (m_timer < 0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
}
