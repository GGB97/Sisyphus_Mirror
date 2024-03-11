using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Player", menuName = "Characters/Player")]

public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public CharacterBaseData BaseData {  get; private set; }
    [field: SerializeField] public AttackData AttackData { get; private set; }
    [field: SerializeField] public HealthData HealthData { get; private set; }
}
