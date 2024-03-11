using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    public EnemyStateMachine stateMachine;

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Awake()
    {
        stateMachine = new(this);

        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
