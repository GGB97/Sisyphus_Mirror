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
            Utilities.AddText(sb,nameof(PhysicalAtk), PhysicalAtk);
        }
        if (MagicAtk != 0)
        {
            Utilities.AddText(sb, nameof(MagicAtk), MagicAtk);
        }
        if (AtkSpeed != 0)
        {
            Utilities.AddText(sb, nameof(AtkSpeed), AtkSpeed);
        }
        if (CritRate != 0)
        {
            Utilities.AddText(sb, nameof(CritRate), CritRate);
        }
        if (CritDamage != 0)
        {
            Utilities.AddText(sb, nameof(CritDamage), CritDamage);
        }
        if (Range != 0)
        {
            Utilities.AddText(sb, nameof(Range), Range);
        }
        if (LifeSteal != 0)
        {
            Utilities.AddText(sb, nameof(LifeSteal), LifeSteal);
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
