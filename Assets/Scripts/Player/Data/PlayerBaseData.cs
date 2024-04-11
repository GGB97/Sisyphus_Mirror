using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBaseData : CharacterBase
{
    [Header("AddInfo")]
    //public float dashRange;
    //public float dashRate;

    //public float critRate;
    //public float critDamage;

    //public float lifeSteal;

    public int LV;
    public float EXP;
    public float maxEXP;
    public int Gold;
    public int Weight;

    public int startItemID;
    public int startInventory;
}
