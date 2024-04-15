using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBaseData : CharacterBase
{
    public int Gold;
    public int LV;
    public float EXP;
    public float maxEXP;

    public override void Init()
    {
        base.Init();
        LV = _lv;
        EXP = _exp;
        maxEXP = _maxEXP;
        Gold = 0;
    }
}
