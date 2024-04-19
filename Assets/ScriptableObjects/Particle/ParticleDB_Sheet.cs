using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Particle/Data")]
public class ParticleDB_Sheet : ScriptableObject
{
	public List<ParticleData> Entities;
}
