using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine playerstateMachine) : base(playerstateMachine)
    {
    }

    private Vector3 movementDirection;
    private float dashTime = 0;
    public override void Enter()
    {
        movementDirection = stateMachine.Player.transform.TransformDirection(Vector3.forward);     // 대시 이동할 방향 로컬좌표를 이용해 바라보는 방향으로 대시
        dashTime = curState.dashRange;                      // 대시 지속시간 초기화
        player.Controller.detectCollisions = false;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.DashParameterHash);
        stateMachine.MovementSpeedModifier = 3f;
        
    }

    public override void Exit()
    {
        base.Exit();
        player.Controller.detectCollisions = true;
        stateMachine.DashCoolTime = 0;
        StopAnimation(stateMachine.Player.AnimationData.DashParameterHash);

    }

    public override void Update() // 대시 이동 정해진 방향으로 일정 시간 동안 이동
    {
        
        dashTime -= Time.deltaTime;  // 대시 지속 시간
        if (dashTime > 0)
        {
            float movementSpeed = curState.moveSpeed * stateMachine.MovementSpeedModifier;
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
