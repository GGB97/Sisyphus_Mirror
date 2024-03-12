using System;
using UnityEngine;

[Serializable]
public abstract class ItemSO
{
    //public int Id => _id;
    //public string Name => _name;
    //public string Description => _description;
    //public string Icon => _icon;
    //public int IconWidth => _iconWidth;
    //public int IconHeight => _iconHeight;
    //public string ItemPrefab => _itemPrefab;
    //public int Price => _price;

    //[field: Header("ItemInfo")]
    //[SerializeField] int _id;
    //[SerializeField] string _name;
    //[SerializeField] string _description;
    //[SerializeField] string _icon;
    //[SerializeField] int _iconWidth;
    //[SerializeField] int _iconHeight;
    //[SerializeField] string _itemPrefab;
    //[SerializeField] int _price;

    [field: Header("ItemInfo")]
    public int id;
    public string itemName;
    public string description;
    public string icon;
    public int iconWidth;
    public int iconHeight;
    public string itemPrefab;
    public int price;

    //public abstract ItemBase CreateItem();
}
