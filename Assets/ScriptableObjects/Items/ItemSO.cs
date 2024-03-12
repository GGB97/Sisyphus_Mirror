using System;
using UnityEngine;

[Serializable]
public abstract class ItemSO
{
    [field: Header("ItemInfo")]
    public int id;
    public string itemName;
    public string description;
    public string icon;
    public int iconWidth;
    public int iconHeight;
    public string itemPrefab;
    public int price;
}
