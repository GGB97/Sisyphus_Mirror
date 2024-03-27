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
        // 여기서 조건에 따라 기본공격/스킬공격의 상태를 전환?
        // 근데 그러면 불필요한 Enter/Exit이 한번 늘어나는거 같은데
        // 하지만 불필요한 Enter/Exit 한번으로 다른 상태들을 고치지 않고 AttackState만 추가로 작성하면
        // 다른 추가 공격상태에 대한 구현이 가능할 것 같음.
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
