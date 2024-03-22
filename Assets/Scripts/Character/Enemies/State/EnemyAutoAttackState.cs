using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoAttackState : EnemyAttackState
{
    public EnemyAutoAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(EnemyAnimData.AutoAttackParameterHash);
        enemy.attackDelay = 0;
    }

    public override void Update()
    {
        base.Update();

        // Attack이 재생중이 아니라면 (공격이 끝났다면)
        if (IsAttacking() == false)
        {
            if (enemy.attackDelay > 0.25f)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimData.AutoAttackParameterHash);
    }

    protected bool IsAttacking() // true == 공격 진행중
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == EnemyAnimData.AutoAttackStateHash)
        {
            // Attack애니메이션이 어느정도 진행되었을때만.
            return !(stateInfo.normalizedTime > 0.98f);
        }

        return false;
    }

    void LookTarget() // 대상을 즉시 바라봄
    {
        Quaternion targetRotation = LookTargetPos();

        // 바라보는 방향 수정
        enemy.transform.rotation = targetRotation;
    }
}
