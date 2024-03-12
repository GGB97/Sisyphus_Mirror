using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStare : PlayerBaseState
{
    public PlayerDashStare(PlayerStateMachine playerstateMachine) : base(playerstateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeed = playerData.DashRange;
        base.Enter();
    //    StartAnimation(stateMachine.Player.AnimationData.DashParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
      //  StopAnimation(stateMachine.Player.AnimationData.DashParameterHash);
    }
}
