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
    public float MovementSpeed { get; set; }

    public Transform PlayerTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        dashState = new PlayerDashStare(this);

        PlayerTransform = player.transform;

        MovementSpeed = player.Data.playerBaseStat.moveSpeed;

    }
}
