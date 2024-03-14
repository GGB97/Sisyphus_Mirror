using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player {  get; }

    public PlayerIdleState idleState { get; }
    public PlayerWalkState walkState { get; }
    public PlayerDashStare dashState { get; }
    
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }        // 기본 이동 속도
    public float MovementSpeedModifier { get; set; } = 1f;  // 상태에 따른 이동속도 결정
    public float DashRange { get; set; }                    // 대시 지속 시간
    public float DashRate { get;  set; }                    // 대사 쿨타임

    public Transform PlayerTransform { get; set; }          

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        dashState = new PlayerDashStare(this);

        PlayerTransform = player.transform;

        MovementSpeed = player.Data.moveSpeed;
        DashRange = player.Data.DashRange;
        DashRate = player.Data.DashRate;
    }
}
