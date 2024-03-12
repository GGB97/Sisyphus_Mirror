using System;
using UnityEngine;

[Serializable]
public class RangeWeaponData : WeaponData
{
    [field: Header("RangeWeapon")]
    public string projectile;
    public int numberOfProjectile;
}
