using UnityEngine;

public class MeleeWeaponData : WeaponData
{
    public override ItemBase CreateItem()
    {
        return new MeleeWeapon(this);
    }
}
