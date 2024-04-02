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
    public static readonly string ProjectileDB = "Projectile/_Data/ProjectileDB_Sheet";
}

public enum ProjectileID
{
    None,
    // 에러 수정용 임시 Arrow
    Arrow = 40000000,
    Cube = 40000001,
    // -----

    OrangeExplosion = 40000100,
    Poison,

    // AreaAttack
    ArearAttack_Ice = 40000200
}

public enum DamageType
{
    Physical,
    Magic
}
