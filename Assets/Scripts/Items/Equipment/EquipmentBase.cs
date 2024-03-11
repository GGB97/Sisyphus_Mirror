public abstract class EquipmentBase : ItemBase
{
    protected EquipmentBase(ItemSO data) : base(data)
    {
    }

    public abstract void Equip();
    public abstract void UnEquip();
}
 