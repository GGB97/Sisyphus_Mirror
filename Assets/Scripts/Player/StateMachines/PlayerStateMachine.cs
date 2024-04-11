using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player {  get; }

    public PlayerIdleState idleState { get; }
    public PlayerWalkState walkState { get; }
    public PlayerDashState dashState { get; }
    public PlayerHitState hitState { get; }
    public PlayerDieState dieState { get; }
    
    public Transform CameraTransform { get; set; }

    public Vector2 MovementInput { get; set; }
    
    public float MovementSpeedModifier = 1f;                // 상태에 따른 이동속도 결정
    public float DashCoolTime = 0f;                         // 대시 쿨타임
           

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        dashState = new PlayerDashState(this);
        hitState = new PlayerHitState(this);
        dieState = new PlayerDieState(this);

        CameraTransform = Camera.main.transform;
    }
}
