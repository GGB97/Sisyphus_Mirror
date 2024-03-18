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
    [SerializeField] private float _physicalAtk;
    [SerializeField] private float _magicAtk;
    [SerializeField] private float _atkSpeed;
    [SerializeField] private float _critRate;
    [SerializeField] private float _critDamage;
    [SerializeField] private float _range;
    [SerializeField] private int _lifeSteal;
    [SerializeField] private WeaponType _type;

    [field: Header("Projectile")]
    [SerializeField] private string _projectileTag;
    [SerializeField] private int _numberOfProjectile;

    public float PhysicalAtk => _physicalAtk;
    public float MagicAtk => _magicAtk;
    public float AtkSpeed => _atkSpeed;
    public float CritRate => _critRate;
    public float CritDamage => _critDamage;
    public float Range => _range;
    public int LifeSteal => _lifeSteal;
    public WeaponType Type => _type;

    public string ProjectileTag => _projectileTag;
    public int NumberOfProjectile => _numberOfProjectile;
}
