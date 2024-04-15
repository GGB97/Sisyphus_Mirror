using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine playerstateMachine) : base(playerstateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.hitDelay = 0;
        StartAnimation(stateMachine.Player.AnimationData.HitParameterHash);
        //Debug.Log("damage");
        //  player.Controller.detectCollisions = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.HitParameterHash);
        player.isHit = false;
        //    player.Controller.detectCollisions = true;
    }

    public override void Update()
    {
        base.Update();
        player.hitDelay += Time.deltaTime;
        if (player.hitDelay > 0.5f)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        if (player.currentStat.health <= 0)
        {
            stateMachine.ChangeState(stateMachine.dieState);
        }
    }
}
