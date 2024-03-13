using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimationData.HitParameterHash);
    }

    public override void Update()
    {
        UpdateTime();

        if (enemy.isDie)
        {
            enemy.InvokeEvent(enemy.OnDieEvent);
        }

        if (!IsTakingHit())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
            
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimationData.HitParameterHash);
        enemy.isHit = false;
    }

    protected bool IsTakingHit()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.shortNameHash == EnemyAnimationData.HitStateHash;
    }
}
