using UnityEngine;

public struct EnemyAnimData
{
    //private static string idleParameterName = "Idle";
    //private static string moveParameterName = "Move";
    //private static string attackParameterName = "Attack";
    //private static string hitParameterName = "Hit";
    //private static string dieParameterName = "Die";

    #region Parameter
    public static readonly int IdleParameterHash = Animator.StringToHash("Idle");
    public static readonly int IdleFloatParameterHash = Animator.StringToHash("Idle_Float");

    public static readonly int MoveParameterHash = Animator.StringToHash("Move");
    public static readonly int MoveSpeedParameterHash = Animator.StringToHash("MoveSpeed");

    public static readonly int AttackParameterHash = Animator.StringToHash("@Attack");
    public static readonly int AutoAttackParameterHash = Animator.StringToHash("AutoAttack");
    public static readonly int Skill01ParameterHash = Animator.StringToHash("Skill01");
    public static readonly int AttackSpeedParameterHash = Animator.StringToHash("AttackSpeed");

    public static readonly int HitParameterHash = Animator.StringToHash("Hit");

    public static readonly int DieParameterHash = Animator.StringToHash("Die");
    #endregion

    #region State
    public static readonly int AttackSubStateHash = Animator.StringToHash("@Attack");
    public static readonly int AutoAttackStateHash = Animator.StringToHash("AutoAttack");
    public static readonly int Skill01StateHash = Animator.StringToHash("Skill01");
    public static readonly int HitStateHash = Animator.StringToHash("GetHit");
    #endregion

}
