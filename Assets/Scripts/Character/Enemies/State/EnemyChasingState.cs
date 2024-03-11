using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{
    float chasingDelay;

    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        chasingDelay = 0;
    }

    public override void Enter()
    {
        base.Enter();

        //StartAnimation(EnemyAnimationData.MoveParameterHash);

        agent.speed = stats.moveSpeed;
        agent.SetDestination(enemy.target.position);
    }

    public override void Update()
    {
        base.Update();

        chasingDelay += Time.deltaTime;

        // 매 프레임 호출하면 성능적으로 안좋을거 같아서 등급별로 호출 횟수를 다르게 설정
        if (chasingDelay >= EnemyData.ChasingDelay[(int)stats.rank])
        {
            chasingDelay = 0;
            // 추후 Player의 진행방향과 속도를 가지고 보스 or 정예몹은 Player의 움직임을 예측해서 추적하는 방식으로 해도 괜찮을듯
            agent.SetDestination(enemy.target.position); 
        }

        // target과의 거리가 range 이하라면 == 공격이 가능한거리라면
        if (Vector3.Distance(agent.destination, enemy.transform.position) <= stats.attackRange)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(EnemyAnimationData.MoveParameterHash);

        agent.ResetPath(); // 추적을 멈추기 위해서
    }
}
