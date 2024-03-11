using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //StartAnimation(EnemyAnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // target과의 거리가 range 이하라면 AttackState로 전환 예정

    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(EnemyAnimationData.MoveParameterHash);
    }
}
