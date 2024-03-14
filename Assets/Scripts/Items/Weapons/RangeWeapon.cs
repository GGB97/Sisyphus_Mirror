using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Transform _weaponPivot;

    private Transform _target;

    protected override void Start()
    {
        base.Start();
        _weaponPivot = GetComponentInChildren<Transform>();
    }

    protected override void Update()
    {
        if(isAttackStart) Attack();
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("attack");
        ObjectPoolManager.Instance.SpawnFromPool("Arrow", _weaponPivot.position, _weaponPivot.rotation);

        isAttackStart = false;
        canAttack = true;
    }
}
