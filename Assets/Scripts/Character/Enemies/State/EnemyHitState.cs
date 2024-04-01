using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimData.HitParameterHash);
        enemy.knockbackDelay = 0f;
    }

    public override void Update()
    {
        UpdateTime();

        if (enemy.knockbackDelay >= EnemyData.KnockBackDelayTime)
        {
            if (IsTakingHit() == false)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimData.HitParameterHash);
        enemy.isHit = false;
    }

    protected bool IsTakingHit()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == EnemyAnimData.HitStateHash)
        {
            // 애니메이션이 어느정도 진행되었을때만.
            return !(stateInfo.shortNameHash == EnemyAnimData.HitStateHash);
        }
        return false;
    }
}
