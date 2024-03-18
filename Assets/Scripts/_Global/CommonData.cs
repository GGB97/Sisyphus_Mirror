using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData
{

}

public struct LayerData
{
    public static readonly LayerMask Terrain = 1 << LayerMask.NameToLayer("Terrain"); // 임시 Layer들
    public static readonly LayerMask Water = 1 << LayerMask.NameToLayer("Water");

    public static readonly LayerMask Player = 1 << LayerMask.NameToLayer("Player");
    public static readonly LayerMask Enemy = 1 << LayerMask.NameToLayer("Enemy");

    public static readonly LayerMask Projectile = 1 << LayerMask.NameToLayer("Projectile");
}

public struct DBPath
{
    public static readonly string EnemyDB = "Enemy/Data/EnemyDB_Sheet";
    public static readonly string ProjectileDB = "Enemy/Data/EnemyDB_Sheet";
}

public enum ProjectileID
{
    OrangeExplosion,
    Poison,
}
