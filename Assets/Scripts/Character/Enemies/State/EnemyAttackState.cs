using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    protected float attackDelay = 10f; // 0으로 시작하면 첫타를 얼타고 있을거 같아서.

    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //StartAnimation(EnemyAnimationData.AttackParameterHash);

        Debug.Log("is Attacking...");
    }

    public override void Update()
    {
        base.Update();

        attackDelay += Time.deltaTime;

        // target과의 거리가 range 보다 크다면 사거리 밖에 있으니 추적
        if (Vector3.Distance(enemy.target.position, enemy.transform.position) > stats.attackRange)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else // 사거리 밖에 있는게 아니면 사거리 이내에 있다는 뜻이니까
        {
            if (attackDelay >= 1 / enemy.Stat.attackSpeed) // 공격속도에 의해서 공격가능 판정
            {
                attackDelay = 0;
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

        // target이 null이면 (죽었으면?) idle로 전환
        if (enemy.target == null)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(EnemyAnimationData.AttackParameterHash);
    }
}
