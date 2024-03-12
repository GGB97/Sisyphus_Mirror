public abstract class ItemBase
{
    public ItemSO Data { get; private set; }

    public ItemBase(ItemSO data) => Data = data;
}
