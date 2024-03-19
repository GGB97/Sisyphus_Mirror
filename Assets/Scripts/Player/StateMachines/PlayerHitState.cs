using System.Collections;
using System.Collections.Generic;
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
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.HitParameterHash);
    }

    public override void Update()
    {
        base.Update();
        player.hitDelay += Time.deltaTime;
        if(player.hitDelay > 0.5f) 
        {

        }
    }
}
