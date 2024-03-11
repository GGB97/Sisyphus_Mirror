using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    public EnemyType type; // 종족
    public EnemyRank rank; // 등급

    public Transform target;
}
