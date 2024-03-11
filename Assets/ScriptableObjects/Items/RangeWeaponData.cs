using UnityEngine;

[CreateAssetMenu(fileName = "RW_", menuName = "Items/Weapon/RangeWeapon", order = 3)]
public class RangeWeaponData : WeaponData
{
    public GameObject Projectile => _projectile;
    public int NumberOfProjectile => _numberOfProjectile;

    [field: Header("RangeWeapon")]
    [SerializeField] GameObject _projectile;
    [SerializeField] int _numberOfProjectile;

    public override ItemBase CreateItem()
    {
        _type = WeaponType.RangeAttack;
        return new RangeWeaponBase(this);
    }
}
