using System;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Range
}

[Serializable]
public class WeaponData : ItemSO
{
    [field: Header("Weapon")]
    public float atk;
    public float atkRate;
    public float critRate;
    public float critDamage;
    public float range;
    public int lifeSteal;
    public WeaponType type;
    public int weaponTier;

    [field: Header("Projectile")]
    public string projectile;
    public int numberOfProjectile;
}
