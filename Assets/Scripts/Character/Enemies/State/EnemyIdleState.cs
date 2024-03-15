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

        StartAnimation(EnemyAnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.target == null)
            return;

        // 공격이 가능하다면
        if (CanAttack())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        // target이 존재하고 공격 사거리 안에 없다면
        if (HasTarget() && !TargetInRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        if (TargetInRange()) // 사거리 내에 target이 있을때는 그녀석을 바라보게
        {
            //LookTargetSlerp();
        }
            
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimationData.IdleParameterHash);
    }

    void LookTargetSlerp() // 대상을 천천히 바라봄
    {
        Quaternion targetRotation = LookTargetPos();

        // 바라보는 방향 수정
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.rotationSpeed * Time.deltaTime); // 보간
    }
}
