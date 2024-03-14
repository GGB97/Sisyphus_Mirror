using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo : CharacterBase
{
    [Header("Enemy Info")]
    public EnemyType type; // 종족
    public EnemyRank rank; // 등급
    public EnemySize size; // 크기

    public EnemyInfo()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }
}
