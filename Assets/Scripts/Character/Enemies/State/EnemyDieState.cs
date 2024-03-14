using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState
{
    public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimationData.DieParameterHash);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimationData.DieParameterHash);
        enemy.isDie = false;
    }
}
