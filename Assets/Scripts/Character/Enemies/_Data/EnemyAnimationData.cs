using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyAnimationData
{
    private static string idleParameterName = "Idle";
    private static string moveParameterName = "Move";
    private static string attackParameterName = "Attack";


    public static readonly int IdleParameterHash = Animator.StringToHash(idleParameterName);
    public static readonly int MoveParameterHash = Animator.StringToHash(moveParameterName);
    public static readonly int AttackParameterHash = Animator.StringToHash(attackParameterName);
}
