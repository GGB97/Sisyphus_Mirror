using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyAnimationData
{
    //private static string idleParameterName = "Idle";
    //private static string moveParameterName = "Move";
    //private static string attackParameterName = "Attack";
    //private static string hitParameterName = "Hit";
    //private static string dieParameterName = "Die";

    #region Parameter
    public static readonly int IdleParameterHash = Animator.StringToHash("Idle");

    public static readonly int MoveParameterHash = Animator.StringToHash("Move");
    public static readonly int MoveSpeedParameterHash = Animator.StringToHash("MoveSpeed");

    public static readonly int AttackParameterHash = Animator.StringToHash("Attack");
    public static readonly int AttackSpeedParameterHash = Animator.StringToHash("AttackSpeed");

    public static readonly int HitParameterHash = Animator.StringToHash("Hit");

    public static readonly int DieParameterHash = Animator.StringToHash("Die");
    #endregion

    #region State
    public static readonly int AttackStateHash = Animator.StringToHash("Attack");
    public static readonly int HitStateHash = Animator.StringToHash("GetHit");
    #endregion

}
