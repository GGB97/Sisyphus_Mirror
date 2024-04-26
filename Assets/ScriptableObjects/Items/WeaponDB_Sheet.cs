using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Items/Data")]
public class WeaponDB_Sheet : ScriptableObject
{
    public List<WeaponData> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
