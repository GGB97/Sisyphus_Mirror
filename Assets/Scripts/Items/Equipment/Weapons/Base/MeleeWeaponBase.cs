using UnityEngine;

public class MeleeWeaponBase : WeaponBase
{
    public MeleeWeaponData MeleeWeaponData { get; private set; }

    public MeleeWeaponBase(MeleeWeaponData data) : base(data)
    {
        MeleeWeaponData = data;
    }

    public override void Equip()
    {
        
    }

    public override void UnEquip()
    {
        
    }

    public override void Attack()
    {
        Debug.Log("Attack");
    }
}
