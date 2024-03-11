using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public Transform target;

    public int id; // DB에서 가져올 ID
    public EnemyBaseStat Stat; //{ get; private set; }

    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Controller = GetComponent<CharacterController>();

        stateMachine = new(this);

        //Stat = DataBase.EnemyStats.Get(id);
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
