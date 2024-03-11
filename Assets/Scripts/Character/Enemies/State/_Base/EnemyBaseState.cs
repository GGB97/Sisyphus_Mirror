using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    protected Enemy enemy;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected CharacterController controller;
    protected EnemyBaseStat stats;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;

        enemy = stateMachine.Enemy;
        animator = enemy.Animator;
        agent = enemy.Agent;
        controller = enemy.Controller;
        stats = enemy.Stat;
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

    }

    protected void StartAnimation(int animationHash)
    {
        animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        animator.SetBool(animationHash, false);
    }
}
