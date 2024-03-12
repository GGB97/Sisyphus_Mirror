using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Items/Data")]
public class MeleeWeaponDB_Sheet : ScriptableObject
{
	public List<MeleeWeaponData> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
