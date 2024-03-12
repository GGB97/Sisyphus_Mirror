using System;
using UnityEngine;

[Serializable]
public class RangeWeaponData : WeaponData
{
    //public string Projectile => _projectile;
    //public int NumberOfProjectile => _numberOfProjectile;

    //[field: Header("RangeWeapon")]
    //[SerializeField] string _projectile;
    //[SerializeField] int _numberOfProjectile;

    [field: Header("RangeWeapon")]
    public string projectile;
    public int numberOfProjectile;

    //public override ItemBase CreateItem()
    //{
    //    _type = WeaponType.RangeAttack;
    //    return new RangeWeaponBase(this);
    //}
}
