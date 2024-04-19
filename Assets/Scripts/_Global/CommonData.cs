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
    public static readonly string PlayerUpgradeDB = "Task/Upgrade/Data/PlayerUpgradeDB_Sheet";
    public static readonly string QuestDB = "Quest/QuestSO";

    public static readonly string LogPath = "mylog.txt";
}

public struct SceneName
{
    public static readonly string Start = "StartTestScene";
    public static readonly string Lobby = "TestLobby";
    public static readonly string Dungeon = "TestScene";
}

public enum GameState
{
    Lobby,
    Dungeon
}

public enum DungeonState
{
    Ready,
    Playing,
    Fail,
    Clear
}

public struct StageTimeLimit
{
    public static readonly float Normal = 30;
    public static readonly float Boss = 60;
}

public enum ProjectileID
{
    None,
    // 에러 수정용 임시 Arrow
    Arrow = 40000000,
    Cube = 40000001,
    Arrow_Ricochet = 40000002,
    // -----

    OrangeExplosion = 40000100,
    Poison = 40000101,
    Electro = 40000102,

    // AreaAttack
    AreaAttack_Ice = 40000200
}

public enum DamageType
{
    Physical,
    Magic,
    Heal
}

#region Task
public enum TaskType
{
    Upgrade
}

public enum UpgradeType
{
    PhysicalAtk = 50000000,
    MagicAtk = 50000001,
    MaxHP = 50000002,
    StartGold = 50000003,
}
#endregion


public struct EnemyStageModifier
{
    public static readonly int maxHealth = 2;
    public static readonly float bossMaxHealth = 0.75f;

    public static readonly float physicalAtk = 0.1f;
    public static readonly float magicAtk = 0.1f;

    public static readonly int gold = 1;

    public static readonly float fieldItemDropPer = 0.05f;
}