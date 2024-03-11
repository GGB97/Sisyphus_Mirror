using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData 
{
    [field: Header("Attack Data")]

    [field: SerializeField] public int Attack { get; private set; } = 0;
    [field: SerializeField] public float AttackSpeed { get; private set; } = 0f;
    [field: SerializeField] public float AttackRange { get; private set; } = 0f;
    [field: SerializeField] public float CriticalRate { get; private set; } = 0f;
    [field: SerializeField] public float CriticalDamage { get; private set; } = 0f;
    [field: SerializeField] public int LifeSteal { get; private set; } = 0;

    [field: Header("Knockback")]
    [field: SerializeField] public bool IsKnockback { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; } = 1f;
}
