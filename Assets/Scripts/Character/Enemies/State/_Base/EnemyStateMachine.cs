public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; private set; }

    // States
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChasingState ChasingState { get; private set; }
    public EnemyAutoAttackState AttackState { get; private set; }
    public EnemySkill01State Skill01State { get; private set; }
    public EnemyHitState HitState { get; private set; }
    public EnemyDieState DieState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        IdleState = new(this);
        ChasingState = new(this);
        AttackState = new(this);
        Skill01State = new(this);
        HitState = new(this);
        DieState = new(this);
    }
}
