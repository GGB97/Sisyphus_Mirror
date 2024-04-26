using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Projectile/_Data")]
public class ProjectileDB_Sheet : ScriptableObject
{
    public List<ProjectileData> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
