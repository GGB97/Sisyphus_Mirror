using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //StartAnimation(EnemyAnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // target과의 거리가 range 이하라면 == 공격이 가능한거리라면
        if (Vector3.Distance(enemy.target.position, enemy.transform.position) <= stats.attackRange)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        // target이 죽어있는 상태? 혹은 그냥 존재한다면 ChasingState로 전환
        if (enemy.target != null)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
    }

    public override void Exit() 
    { 
        base.Exit();

        //StopAnimation(EnemyAnimationData.IdleParameterHash);
    }
}
