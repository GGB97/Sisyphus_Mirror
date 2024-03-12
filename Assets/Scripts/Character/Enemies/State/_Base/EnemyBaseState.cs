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

    protected float chasingDelay = 0f;
    protected float attackDelay = 0f; // 0으로 시작하면 첫타를 얼타고 있을거 같아서.

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
        UpdateTime();
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

    protected bool IsAttacking()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.shortNameHash == EnemyAnimationData.AttackParameterHash;
    }

    void UpdateTime()
    {
        if(attackDelay < 10f)
            attackDelay += Time.deltaTime;
    }
}
