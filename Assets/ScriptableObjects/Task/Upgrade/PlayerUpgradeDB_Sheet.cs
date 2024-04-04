using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Task/Upgrade/Data")]
public class PlayerUpgradeDB_Sheet : ScriptableObject
{
    public List<UpgradeData> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
