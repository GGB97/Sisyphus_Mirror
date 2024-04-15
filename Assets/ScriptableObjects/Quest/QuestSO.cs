using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Quest", ExcelName = "QuestDataSheet")]
public class QuestSO : ScriptableObject
{
    public List<QuestData> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
