public abstract class EquipmentBase : ItemBase
{
    protected EquipmentBase(ItemData data) : base(data)
    {
    }

    public abstract void Equip();
    public abstract void UnEquip();
}
 