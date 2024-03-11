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

        // target이 죽어있는 상태? 혹은 그냥 존재한다면 ChasingState로 전환 예정
        if(enemy.target != null)
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
