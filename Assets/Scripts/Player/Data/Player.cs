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
    public event Action<float> PlayerSheildChange;


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
        Data.Init();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        HealthSystem = GetComponent<HealthSystem>();

        rune = PlayerPrefs.GetInt("Rune"); // 나중에 저장해야함.

        stateMachine = new PlayerStateMachine(this);

        SetUpgradeModifier();
    }

    private void OnEnable()
    {
        if (_gameManager.gameState == GameState.Dungeon)
        {
            if (_dungeonManager == null)
            {
                _dungeonManager = DungeonManager.Instance;
            }

            currentStat.SyncHealth();

            _dungeonManager.OnStageStart += ResetMagnet;
            _dungeonManager.OnStageStart += ResetShield;
            _dungeonManager.OnStageStart += UnInvincibility;

            _dungeonManager.OnStageClear += Invincibility;
            _dungeonManager.OnStageClear += StageClearGetitem;
        }
    }

    private void OnDisable()
    {
        if (_gameManager.gameState == GameState.Dungeon)
        {
            if (_dungeonManager != null)
            {
                _dungeonManager.OnStageStart -= ResetMagnet;
                _dungeonManager.OnStageStart -= ResetShield;
                _dungeonManager.OnStageStart -= UnInvincibility;

                _dungeonManager.OnStageClear -= Invincibility;
                _dungeonManager.OnStageClear -= StageClearGetitem;
            }
        }
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.idleState);

        currentStat.InitStatus(Data, modifire);
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
        InvokeShieldChange();
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
        stateMachine.ChangeState(stateMachine.dieState);
    }

    void ChangeHitState()
    {
        InvokeShieldChange();
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
        stateMachine.ChangeState(stateMachine.hitState);
    }

    public void ChangeRune(int value)
    {
        rune += value;
        PlayerPrefs.SetInt("Rune", rune);
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

    public void InvokeShieldChange()
    {
        int maxShield = Mathf.RoundToInt(currentStat.maxHealth * 0.2f);
        if (currentStat.shield > maxShield)
        {
            currentStat.shield = maxShield;
        }
        PlayerSheildChange?.Invoke(currentStat.shield);
    }

    public void GetEXP(int exp)
    {
        Data.EXP += exp;
        if (Data.EXP >= Data.maxEXP)
        {
            LevelUp();
        }
        GameManager.Instance.killenemys++;
        PlayerExpChange?.Invoke(Data.EXP, Data.maxEXP);
    }

    public void LevelUp()
    {
        Data.EXP = Data.EXP - Data.maxEXP;
        Data.LV++;
        Data.maxEXP = Data._maxEXP + ((Data.LV - 1) * 10);
        currentStat.maxHealth = currentStat.maxHealth + 2;
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
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
        Data.Init();
    }

    void StageClearGetitem()
    {
        magnetDistance = 100;
    }

    void ResetMagnet()
    {
        magnetDistance = 3;
    }
    void ResetShield()
    {
        currentStat.shield = 0;
        InvokeShieldChange();
    }

    public void HealthChange()
    {
        if (currentStat.health > currentStat.maxHealth) currentStat.health = currentStat.maxHealth;
        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
    }

    public void HealthChange(int _health)
    {
        if (currentStat.health + _health > currentStat.maxHealth)
            currentStat.health = currentStat.maxHealth;
        else currentStat.health += _health;

        PlayerHealthChange?.Invoke(currentStat.maxHealth, currentStat.health);
    }

    void Invincibility()
    {
        LayerMask excludeTarget = LayerData.Enemy | LayerData.Projectile;
        Controller.excludeLayers = excludeTarget;
    }

    void UnInvincibility()
    {
        Controller.excludeLayers = 0;
    }
}
