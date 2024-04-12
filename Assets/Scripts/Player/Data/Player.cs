using System;
using UnityEngine;

public class Player : CharacterBehaviour
{
    GameManager _gameManager;
    DungeonManager _dungeonManager;

    [field: Header("References")]
    [field: SerializeField] public PlayerBaseData Data { get; private set; }
    public Status modifire;

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public HealthSystem HealthSystem;

    private PlayerStateMachine stateMachine;

    public float hitDelay;

    public event Action<float, float> PlayerHealthChange;
    public float health;
    public event Action<float, float> PlayerExpChange;

    public int rune;
    public event Action PlayerRuneChange;

    public event Action PlayerGoldChange;

    public float magnetDistance;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        AnimationData.Initialize();
        Data = DataBase.Player.Get(id);
        currentStat.InitStatus(Data, modifire);
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        HealthSystem = GetComponent<HealthSystem>();

        rune = 10000; // 나중에 저장해야함.

        stateMachine = new PlayerStateMachine(this);
    }

    private void OnEnable()
    {
        if (_gameManager.gameState == GameState.Dungeon)
        {
            if (_dungeonManager == null)
            {
                _dungeonManager = DungeonManager.Instance;
            }

            _dungeonManager.OnStageEnd += StageClearGetitem;
        }
    }

    private void OnDisable()
    {
        if (_gameManager.gameState == GameState.Dungeon)
        {
            if (_dungeonManager != null)
            {
                _dungeonManager.OnStageEnd -= StageClearGetitem;
            }
        }
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

    public void ChangeDieState()
    {
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
        stateMachine.ChangeState(stateMachine.dieState);
    }

    void ChangeHitState()
    {
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
        stateMachine.ChangeState(stateMachine.hitState);
    }

    public void ChangeRune(int value)
    {
        rune += value;
        PlayerRuneChange?.Invoke();
    }

    public void ChangeGold(int value)
    {
        Data.Gold += value;
        PlayerGoldChange?.Invoke();
    }

    public void InvokeEvent(Action action)
    {
        action?.Invoke();
    }

    public void GetEXP(int exp)
    {
        Data.EXP += exp;
        if (Data.EXP >= Data.maxEXP)
        {
            Data.EXP = 0;
            Data.LV++;
        }
        GameManager.Instance.killenemys++;
        PlayerExpChange?.Invoke(Data.EXP, Data.maxEXP);
    }

    public void SetUpgradeModifier() // 던전 입장시 실행해야하고 currentStatus 초기화 전 실행해야할듯.
    {
        modifire.physicalAtk += DataBase.PlayerUpgrade.Get((int)UpgradeType.PhysicalAtk).Reward;
        modifire.magicAtk += DataBase.PlayerUpgrade.Get((int)UpgradeType.MagicAtk).Reward;

        modifire.maxHealth += DataBase.PlayerUpgrade.Get((int)UpgradeType.MaxHP).Reward;
        Data.Gold += (int)DataBase.PlayerUpgrade.Get((int)UpgradeType.StartGold).Reward;
    }


    public void playerReset()
    {
        Data.LV = 1;
        Data.Gold = 0;
        Data.EXP = 0;
    }

    void StageClearGetitem(int dump)
    {
        magnetDistance = 100;

    }
}
