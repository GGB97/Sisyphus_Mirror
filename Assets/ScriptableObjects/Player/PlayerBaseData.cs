using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBaseData : CharacterBaseStat
{
    public float DashRange;
    public float DashRate;

    public float CritRate;
    public float CritDamage;

    public float LifeSteal;

    public int LV;
    public float EXP;
    public float maxEXP;

}
