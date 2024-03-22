using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill01State : EnemyAttackState
{
    public EnemySkill01State(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimData.Skill01ParameterHash);
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

        StopAnimation(EnemyAnimData.Skill01ParameterHash);
    }

    protected bool IsAttacking() // true == 공격 진행중
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == EnemyAnimData.Skill01StateHash)
        {
            // 애니메이션이 어느정도 진행되었을때만.
            return !(stateInfo.normalizedTime > 0.98f);
        }

        return false;
    }
}
