using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Enemy/Data")]
public class EnemyDB_Sheet : ScriptableObject
{
	public List<EnemyBaseStat> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
