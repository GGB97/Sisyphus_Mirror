using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimationData.AttackParameterHash);

        enemy.transform.LookAt(enemy.target.transform);
        enemy.attackDelay = 0;
    }

    public override void Update()
    {
        base.Update();

        // Attack이 재생중이 아니라면 (공격이 끝났다면)
        if (!IsAttacking())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // target이 null이면 (죽었으면?) idle로 전환
        if (HasTarget() == false)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimationData.AttackParameterHash);
    }

    protected bool IsAttacking()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.shortNameHash == EnemyAnimationData.AttackStateHash;
    }
}
