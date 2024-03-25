using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Items/Data")]
public class ConsumableDB_Sheet : ScriptableObject
{
	public List<ConsumableData> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
