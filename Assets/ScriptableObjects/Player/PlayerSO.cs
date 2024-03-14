using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Player", menuName = "Characters/Player")]

public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerBaseData playerBaseStat {  get; private set; }
}
