using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MW_", menuName = "Items/Weapon/MeleeWeapon", order = 3)]
[Serializable]
public class MeleeWeaponData : WeaponData
{
    public override ItemBase CreateItem()
    {
        _type = WeaponType.MeleeAttack;
        return new MeleeWeaponBase(this);
    }
}
