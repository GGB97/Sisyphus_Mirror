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

        //StartAnimation(EnemyAnimationData.AttackParameterHash);

        Debug.Log("is Attacking...");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(EnemyAnimationData.AttackParameterHash);
    }
}
