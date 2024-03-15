using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStare : PlayerBaseState
{
    public PlayerDashStare(PlayerStateMachine playerstateMachine) : base(playerstateMachine)
    {
    }

    private Vector3 movementDirection;
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 3f;            
        movementDirection = GetMovementDirection();         // 대시 이동할 방향 
        stateMachine.DashRange = playerData.dashRange;      // 대시 지속시간 초기화
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.DashParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.DashCoolTime = 0;
        StopAnimation(stateMachine.Player.AnimationData.DashParameterHash);

    }

    public override void Update() // 대시 이동 정해진 방향으로 일정 시간 동안 이동
    {
        
        stateMachine.DashRange -= Time.deltaTime;  // 대시 지속 시간
        if (stateMachine.DashRange > 0)
        {
            float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
            stateMachine.Player.Controller.Move(
                (movementDirection * movementSpeed) * Time.deltaTime
                );
        }
        else
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}
