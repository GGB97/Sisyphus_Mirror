using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType // 종족
{
    Slime,
    TurtleShell
}

public enum EnemyRank // 등급
{
    Normal,
    Elite,
    Boss
}

public struct EnemyData
{
    public static readonly string DBPath = "Enemy/Data/EnemyDB_Sheet";

    public static readonly float[] ChasingDelay = { 
        0.2f, // Normal
        0.1f, // Elite
        0.05f  // Boss
    };
}