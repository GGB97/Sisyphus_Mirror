using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Player/Data")]
public class PlayerDB_Sheet : ScriptableObject
{
    public List<PlayerBaseData> Entities; // Replace 'EntityType' to an actual type that is serializable.

}
