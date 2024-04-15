using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Enemy/Data")]
public class EnemyDB_Sheet : ScriptableObject
{
    public List<EnemyInfo> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
