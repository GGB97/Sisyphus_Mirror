using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Player : CharacterBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerBaseData Data {  get; private set; }
    public Status modifire;

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData {  get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public HealthSystem HealthSystem;

    private PlayerStateMachine stateMachine;

    public float hitDelay;

    public event Action<float, float> PlayerHealthChange;
    public float health;

    public int rune;

    private void Awake()
    {
        AnimationData.Initialize();
        Data = DataBase.Player.Get(id);
        currentStat.InitStatus(Data, modifire);
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        HealthSystem = GetComponent<HealthSystem>();

        rune = PlayerPrefs.GetInt(PlayerPrebsString.Rune); // 이걸 Player에? 아니면 GameManager에?

        stateMachine = new PlayerStateMachine(this);  
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.idleState);
        //health = currentStat.maxHealth;
        currentStat.Init();

        isDie = false;
        isHit = false;

        OnDieEvent += ChangeDieState;
        OnHitEvent += ChangeHitState;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void ChangeDieState()
    {
        stateMachine.ChangeState(stateMachine.dieState);
    }

    void ChangeHitState()
    {
        stateMachine.ChangeState(stateMachine.hitState);
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
    }

    public void InvokeEvent(Action action)
    {
        action?.Invoke();
    }
    
}
