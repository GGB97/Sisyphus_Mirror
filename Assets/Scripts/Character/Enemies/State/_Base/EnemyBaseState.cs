using System.Collections;
using System.Collections.Generic;
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

    protected float chasingDelay;
    protected float attackDelay;

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

        if (enemy.isHit)
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

    protected bool HasTarget() // Tartget이 존재하는지 확인
    {
        return (enemy.target != null);
    }

    protected void UpdateTime()
    {
        if(attackDelay < 10f)
            attackDelay += Time.deltaTime;
    }
}
