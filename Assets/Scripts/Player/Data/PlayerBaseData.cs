using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBaseData : CharacterBase
{
    [Header("AddInfo")]

    public int LV = 0;
    public float EXP = 0;
    public float maxEXP = 100;
    public int Gold = 0;
    public int Weight = 0;

    public int startItemID;
    public int startInventory;


}
