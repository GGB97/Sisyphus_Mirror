using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    protected Enemy enemy;
    protected EnemyInfo info;
    protected Status curStat;

    protected Animator animator;
    protected NavMeshAgent agent;
    protected CharacterController controller;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;

        enemy = stateMachine.Enemy;
        info = enemy.Info;
        curStat = enemy.currentStat;

        animator = enemy.Animator;
        agent = enemy.Agent;
        controller = enemy.Controller;

    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        UpdateTime();

        if (enemy.isDie)
        {
            enemy.InvokeEvent(enemy.OnDieEvent);
        }

        if (enemy.isHit && enemy.knockbackDelay > EnemyData.KnockBackDelayTime)
        {
            enemy.InvokeEvent(enemy.OnHitEvent);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        animator.SetBool(animationHash, false);
    }

    protected void UpdateTime()
    {
        if (enemy.attackDelay < 10f)
        {
            enemy.attackDelay += Time.deltaTime;
        }

        if (enemy.knockbackDelay < 3f)
            enemy.knockbackDelay += Time.deltaTime;
    }

    protected bool HasTarget() // Tartget이 존재하는지 확인
    {
        return (enemy.target != null);
    }

    protected bool CanAttack() // 공격이 가능한지
    {
        return TargetInRange() && IsAttackReady();
    }

    protected bool TargetInRange()
    {
        return (Vector3.Distance(enemy.target.position, enemy.transform.position) <= curStat.attackRange);
    }

    protected bool IsAttackReady()
    {
        return (enemy.attackDelay >= 1 / enemy.Info.attackSpeed);
    }

    protected Quaternion LookTargetPos() // 바라볼 방향 계산
    {
        Vector3 directionToLookAt = enemy.target.position - enemy.transform.position;
        directionToLookAt.y = 0; // 수평 회전만 고려

        return Quaternion.LookRotation(directionToLookAt);
    }
}
