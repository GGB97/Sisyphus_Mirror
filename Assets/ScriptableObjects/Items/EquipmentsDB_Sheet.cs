using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Items/Data")]
public class EquipmentsDB_Sheet : ScriptableObject
{
	public List<EquipmentsData> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
