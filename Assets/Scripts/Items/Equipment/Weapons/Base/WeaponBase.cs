public abstract class WeaponBase : EquipmentBase
{
    public WeaponData WeaponData { get; private set; }

    public WeaponBase(WeaponData data) : base(data)
    {
    }

    public abstract void Attack();
}
