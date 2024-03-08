public abstract class WeaponBase : ItemBase
{
    public WeaponData WeaponData { get; private set; }

    public WeaponBase(WeaponData data) : base(data)
    {
        WeaponData = data;
    }
}
