using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterBaseData
{
    [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField] public float MaxEXP { get; private set; } = 100f;
    [field: SerializeField] public float StartGold { get; private set; } = 0f;

    [field: Header("moveData")]

    [field: SerializeField] public float MoveSpeedModifier { get; private set; } = 0.2f;
    [field: SerializeField] public float DashRange { get; private set; } = 5f;
    [field: SerializeField] public float DashRate { get; private set; } = 3f;

}
