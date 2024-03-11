using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int IconWidth => _iconWidth;
    public int IconHeight => _iconHeight;

    [field: Header("ItemInfo")]
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] Sprite _icon;
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] int _iconWidth;
    [SerializeField] int _iconHeight;

    public abstract ItemBase CreateItem();
}
