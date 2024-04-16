public class EnemyDieState : EnemyBaseState
{
    public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(EnemyAnimData.DieParameterHash);
        enemy.Collider.enabled = false;

        EnemySpawner.Instance.DecrementEnemyCnt(); // 죽었으니까 currentEnemyCnt 감소

        if (enemy.isDie == true)
        {
            SoundManager.Instance.PlayAudioClip(enemy.hitSound);
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(EnemyAnimData.DieParameterHash);
        enemy.isDie = false;
        enemy.Collider.enabled = true;
    }
}
