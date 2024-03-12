using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Items/Data")]
public class RangeWeaponDB_Sheet : ScriptableObject
{
	public List<RangeWeaponData> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
