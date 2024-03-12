using System;
using UnityEngine;

[Serializable]
public class ItemSO
{
    [field: Header("ItemInfo")]
    [SerializeField] protected int _id;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected string _spritePath;
    [SerializeField] protected int _iconWidth;
    [SerializeField] protected int _iconHeight;
    [SerializeField] protected string _prefabPath;
    [SerializeField] protected int _price;

    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public string SpritePath => _spritePath;
    public int IconWidth => _iconWidth;
    public int IconHeight => _iconHeight;
    public string PrefabPath => _prefabPath;
    public int Price => _price;
}
