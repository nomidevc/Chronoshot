using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _shootingPointTransform;
    [SerializeField] private Transform _bulletProjectilePrefab;

    static readonly int Shoot = Animator.StringToHash("Shoot");
    static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    void Start()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
            moveAction.OnUnitWalking += MoveAction_OnUnitWalking;
        
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
            shootAction.OnUnitShooting += ShootAction_OnUnitShooting;
        
    }
    private void ShootAction_OnUnitShooting(Unit targetUnit, Unit shootUnit)
    {
        _animator.SetTrigger(Shoot);
        
        Transform bulletProjectileTransform = Instantiate(_bulletProjectilePrefab, 
            _shootingPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        
        Vector3 aimPosition = targetUnit.transform.position;
        aimPosition.y = bulletProjectileTransform.position.y;
        bulletProjectile.SetTargetPosition(aimPosition);
    }
    private void MoveAction_OnUnitWalking(bool isWalking)
    {
        _animator.SetBool(IsWalking, isWalking);
    }
}
