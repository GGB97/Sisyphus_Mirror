using System;
using System.Text;
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
    [SerializeField] private ProjectileID _projectileID;
    [SerializeField] private int _numberOfProjectile;

    public float PhysicalAtk => _physicalAtk;
    public float MagicAtk => _magicAtk;
    public float AtkSpeed => _atkSpeed;
    public float CritRate => _critRate;
    public float CritDamage => _critDamage;
    public float Range => _range;
    public int LifeSteal => _lifeSteal;
    public WeaponType Type => _type;

    public ProjectileID ProjectileID => _projectileID;
    public int NumberOfProjectile => _numberOfProjectile;
    public override StringBuilder SetExplantion(ItemSO itemSO)
    {
        StringBuilder sb = base.SetExplantion(itemSO);
        if (PhysicalAtk != 0)
        {
            Utilities.AddText(sb,"물리 공격력", PhysicalAtk);
        }
        if (MagicAtk != 0)
        {
            Utilities.AddText(sb, "마법 공격력", MagicAtk);
        }
        if (AtkSpeed != 0)
        {
            Utilities.AddText(sb, "공격 속도", AtkSpeed,true);
        }
        if (CritRate != 0)
        {
            Utilities.AddText(sb, "치명타 확률", CritRate,true);
        }
        if (CritDamage != 0)
        {
            Utilities.AddText(sb, "치명타 데미지", CritDamage);
        }
        if (Range != 0)
        {
            Utilities.AddText(sb, "공격 범위", Range,true);
        }
        if (LifeSteal != 0)
        {
            Utilities.AddText(sb, "피해 흡혈", LifeSteal, true);
        }

        return sb;
    }
    //public StringBuilder AddText(StringBuilder sb ,string name , float value)
    //{
    //    sb.Append($"{name} : ");
    //    if (value > 0)
    //    {
    //        sb.Append($"<color=green>{value}</color>\n");
    //    }
    //    else
    //    {
    //        sb.Append($"<color=red>{value}</color>\n");
    //    }
    //    return sb;
    //}
}
