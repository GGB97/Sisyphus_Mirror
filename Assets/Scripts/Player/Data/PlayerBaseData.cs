using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBaseData : CharacterBase
{
    [Header("AddInfo")]

    public int LV;
    public float EXP;
    public float maxEXP;
    public int Gold;
    

    public int startItemID;
    public int startInventory;

    public override void Init()
    {
        base.Init();
        LV = 1;
        EXP = 0;
        maxEXP = 100;
        Gold = 0;
    }
}
