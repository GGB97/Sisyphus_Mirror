using System;
using System.Text;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Equipments,
    Consumable
}

public enum ItemGrade
{
    Normal,
    Rare,
    Unique,
    RuneStone
}

[Serializable]
public class ItemSO
{
    [field: Header("ItemInfo")]
    [SerializeField] protected int _id;
    [SerializeField] protected ItemType _itemType;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected string _spritePath;
    [SerializeField] protected int _iconWidth;
    [SerializeField] protected int _iconHeight;
    [SerializeField] protected string _prefabPath;
    [SerializeField] protected int _price;
    [SerializeField] protected float _weight;
    [SerializeField] protected ItemGrade _grade;

    public int Id => _id;
    public ItemType ItemType => _itemType;
    public string Name => _name;
    public string Description => _description;
    public string SpritePath => _spritePath;
    public int IconWidth => _iconWidth;
    public int IconHeight => _iconHeight;
    public string PrefabPath => _prefabPath;
    public int Price => _price;
    public float Weight => _weight;
    public ItemGrade Grade => _grade;

    private Sprite _sprite;
    public Sprite Sprite
    {
        get
        {
            if (_sprite == null) _sprite = Resources.Load<Sprite>(SpritePath);
            return _sprite;
        }
    }

    private GameObject _prefab;
    public GameObject Prefab
    {
        get
        {
            if (_prefabPath.Equals("None")) return null;
            if (_prefab == null) _prefab = Resources.Load<GameObject>(PrefabPath);
            return _prefab;
        }
    }
    public virtual StringBuilder SetExplantion(ItemSO itemSO)
    {
        StringBuilder sb = new StringBuilder(270);
        sb.Append($"{itemSO.Description}\n");//설명 적기
        Utilities.AddText(sb, "무게", itemSO.Weight, false, true);
        return sb;
    }
}
