using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    static readonly int Shoot = Animator.StringToHash("Shoot");
    static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    void Start()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
            moveAction.OnUnitWalking += MoveAction_OnUnitWalking;
        
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
            shootAction.OnUnitShooting += ShootAction_OnUnitShooting;
        
    }
    private void ShootAction_OnUnitShooting()
    {
        _animator.SetTrigger(Shoot);
    }
    private void MoveAction_OnUnitWalking(bool isWalking)
    {
        _animator.SetBool(IsWalking, isWalking);
    }
}
