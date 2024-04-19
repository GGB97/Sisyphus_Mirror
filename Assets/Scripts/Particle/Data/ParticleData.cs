using System;
using UnityEngine;

[Serializable]
public class ParticleData
{
    [SerializeField] int _id;
    [SerializeField] string _prefabPath;

    public int Id => _id;
    public string PrefabPath => _prefabPath;
}
