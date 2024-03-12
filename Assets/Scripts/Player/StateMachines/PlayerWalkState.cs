using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine playerstateMachine) : base(playerstateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeed = playerData.moveSpeed;
        base.Enter();
       // StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Exit() 
    { 
        base.Exit();
      //  StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    protected override void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.idleState);

        base.OnMoveCanceled(context);
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.dashState);
        base.OnDashStarted(context);
    }

    
}
