using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Tutorials/Data")]
public class TutorialDB_Sheet : ScriptableObject
{
	public List<TutorialData> Entities;
}
