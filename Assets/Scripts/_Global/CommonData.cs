using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData
{

}

public struct LayerData
{
    public static readonly LayerMask Default = 1 << LayerMask.NameToLayer("Default");
    public static readonly LayerMask Water = 1 << LayerMask.NameToLayer("Water"); // 임시 Layer들
}