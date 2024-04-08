public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimData.IdleParameterHash);
    }

    public override void Update()
    {
        if (enemy.IsSpawning)
            return;

        base.Update();

        if (enemy.target == null)
            return;

        // 공격이 가능하다면
        if (CanAttack())
        {
            ChangeAttackState();
            return;
        }

        // target이 존재하고 공격 사거리 안에 없다면
        if (HasTarget() && TargetInRange() == false)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        if (TargetInRange()) // 사거리 내에 target이 있을때는 그녀석을 바라보게
        {
            LookTargetSlerp();
        }
            
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimData.IdleParameterHash);
    }
}
