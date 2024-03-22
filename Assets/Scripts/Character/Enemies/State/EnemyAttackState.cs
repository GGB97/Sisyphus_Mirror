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
        StartAnimation(EnemyAnimData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

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

        StopAnimation(EnemyAnimData.AttackParameterHash);
    }
}
