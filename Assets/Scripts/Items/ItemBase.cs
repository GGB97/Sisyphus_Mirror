public abstract class ItemBase
{
    public ItemData Data { get; private set; }

    public ItemBase(ItemData data) => Data = data;
}
