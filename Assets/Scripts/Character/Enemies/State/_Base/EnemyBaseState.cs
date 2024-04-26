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

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;

        enemy = stateMachine.Enemy;
        info = enemy.Info;
        curStat = enemy.currentStat;

        animator = enemy.Animator;
        agent = enemy.Agent;
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

        if (enemy.isHit)
        {
            enemy.InvokeChangeHealth();
            if (enemy.Info.rank != EnemyRank.Boss && enemy.knockbackDelay > EnemyData.KnockBackDelayTime)
            {
                enemy.InvokeEvent(enemy.OnHitEvent);
                return;
            }
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
        // 사거리 안에 있고 공격 딜레이가 충분히 지났으며 정면에 타겟이 있어야함.
        return TargetInRange() && IsAttackReady() && TargetOnFront();
    }

    protected bool TargetInRange()
    {
        return (Vector3.Distance(enemy.target.position, enemy.transform.position) <= curStat.attackRange);
    }

    protected bool TargetOnFront()
    {
        Vector3 directionToTarget = enemy.target.transform.position - enemy.transform.position;
        directionToTarget.y = 0;
        float angleToTarget = Vector3.Angle(enemy.transform.forward, directionToTarget);

        return angleToTarget < enemy.Info.attackAngle; // 일정 각도안에 target이 있는지 리턴
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

    protected void LookTargetSlerp() // 대상을 천천히 바라봄
    {
        Quaternion targetRotation = LookTargetPos();

        // 바라보는 방향 수정
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.Info.rotationSpeed * Time.deltaTime);
    }

    protected virtual void ChangeAttackState()
    {
        if (enemy.Info.rank == EnemyRank.Boss)
        {
            int rand = Random.Range(0, 10);
            if (rand < 3)
                stateMachine.ChangeState(stateMachine.Skill01State);
            else
                stateMachine.ChangeState(stateMachine.AttackState);

        }
        else
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
