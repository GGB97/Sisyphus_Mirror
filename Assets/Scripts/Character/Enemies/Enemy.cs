using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : CharacterBehaviour
{
    public EnemyStateMachine stateMachine;
    public Transform target;

    public bool IsSpawning { get; private set; }
    public float chasingDelay;
    public float attackDelay;

    [field: SerializeField] public EnemyInfo Info { get; private set; }
    public Status modifier; // 스탯 가중치 (Player의 경우 장비에 의한 가중치, Enemy의 경우 난이도/층수 에 의한 가중치)

    public Collider Collider { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    Renderer[] _enemyRenderer; // 투명도 조절을 위한 mat 
    [SerializeField] Material _baseMat; // 원래 mat저장해두기 위한 용도.
    [SerializeField] Material _spawnMat;

    // 나중에 기본 속도와 추가값에 비례해서 리턴하도록 프로퍼티로 수정하면 될듯
    public float animAttackSpeed = 1f;
    public float animMoveSpeed = 1f;

    [SerializeField] Collider[] _meleeAttackColliders;

    [SerializeField] Transform[] _rangeAttackPos;
    [SerializeField] ProjectileID[] _projectileTag;

    private void Awake()
    {
        Info = DataBase.EnemyStats.Get(id);

        Collider = GetComponent<Collider>();
        Animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();

        _enemyRenderer = GetComponentsInChildren<Renderer>();

        stateMachine = new(this);

        Init();
    }

    private void OnEnable()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        StartSpawn();
    }

    private void OnDisable()
    {
        EnemyPooler.Instance.ReturnToPull(gameObject);
    }

    void Start()
    {
        //stateMachine.ChangeState(stateMachine.IdleState);

        OnDieEvent += ChangeDieState;
        OnDieEvent += InvokeOnDieFadeOut;

        OnHitEvent += ChangeHitState;

        target = EnemySpawner.Instance.target; // 임시
    }

    void Update()
    {
        stateMachine.Update();

        if(DungeonManager.Instance.isStarted == false)
            OnDieEvent?.Invoke();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void Init()
    {
        currentStat.InitStatus(Info, modifier);

        #region AnimatorOverrideController 으로 시도했던것
        // OverrideAnimator는 속도 조절에는 사용하지 않아도 되지만 시도해본 방법중 하나였음.
        // OverrideAnimator는 애니메이션 클립을 부분적으로만 변경을 하려 할 때 사용하기 좋을 것 같음
        // ex)특정 조건 만족시 공격 모션이나 그런게 변할 때?
        //OverrideAnimator = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        //Animator.runtimeAnimatorController = OverrideAnimator;
        #endregion
        Animator.SetFloat(EnemyAnimData.AttackSpeedParameterHash, animAttackSpeed); // 각 객체의 속도별로 Animation의 속도를 조절하기 위해서
        Animator.SetFloat(EnemyAnimData.MoveSpeedParameterHash, animMoveSpeed); // 현재 프로젝트에서는 필요 없을 것 같지만 그냥 해보고싶었음.

        Agent.stoppingDistance = Info.attackRange - .2f; // 사거리보다 살짝 더 들어가게끔 하지 않으면 멈춰서 이상한짓함
        Agent.angularSpeed = 240f;

        isDie = false;
        isHit = false;

        chasingDelay = 10f; // 그냥 초기값 설정
        attackDelay = 10f;
        knockbackDelay = 10f;

        switch (Info.rank) // 등급별로 동적 장애물 회피 성능을 조절해서 최적화?
        {
            case EnemyRank.Normal:
                Agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
                break;
            case EnemyRank.Elite:
                Agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
                break;
            case EnemyRank.Boss:
                Agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                break;
        }
    }

    public void OnChildTriggerEnter(Collider other, SkillType type)
    {
        //이곳에서 자식 콜라이더의 트리거 충돌 처리
        if(type == SkillType.AutoAttack)
            Debug.Log($"AA : {gameObject.name} -> Attack : {other.gameObject.name}");
        else if (type == SkillType.Skill01)
            Debug.Log($"Skill : {gameObject.name} -> Attack : {other.gameObject.name}");
    }

    void ChangeDieState()
    {
        isDie = true;
        stateMachine.ChangeState(stateMachine.DieState);
    }

    void ChangeHitState()
    {
        stateMachine.ChangeState(stateMachine.HitState);
    }

    public void InvokeEvent(Action action)
    {
        action?.Invoke();
    }

    public void AttackStart(int num)
    {
        //Debug.Log("Attack Start");
        _meleeAttackColliders[num].enabled = true;
    }

    public void AttackEnd(int num)
    {
        _meleeAttackColliders[num].enabled = false;
        //Debug.Log("Attack End");
    }

    public void RangedAttack(int num)
    {
        //GameObject go = Instantiate(_projectilePrefabs[prfabNum],
        //    _rangeAttackPos[posNum].transform.position, transform.rotation); // 이걸 오브젝트풀에서 가져오게 하면될듯

        GameObject go = ObjectPoolManager.Instance.SpawnFromPool(
            (int)_projectileTag[num],
            _rangeAttackPos[num].transform.position,
            _rangeAttackPos[num].transform.rotation);

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0f;
        directionToTarget.Normalize();

        ProjectileTest projectile = go.GetComponent<ProjectileTest>();

        projectile.AddTarget(LayerData.Player); // Player를 맞춰야함
        projectile.AddExcludeLayer(LayerData.Enemy); // Enemy는 충돌하지 않게 설정

        float value = projectile.GetDamageType == DamageType.Physical ? currentStat.meleeAtk : currentStat.magicAtk;
        projectile.SetValue(value);

        //projectile.rb.AddForce(directionToTarget * 10f, ForceMode.Impulse);
        projectile.SetVelocity(1f); // 속도 배율 설정
    }

    void StartSpawn()
    {
        IsSpawning = true;

        #region Renderer.sharedMaterial
        // shared를 사용하면 해당 mat을 사용하는 모든 객체들이 변경되어야 하지만 실제로 사용해보니까 그렇게 되지는 않았음.
        // 하지만 shared를 사용하게되면 다른때에 갑자기 다 바뀔수도 있을거 같아서 채택하지 않음.
        // GPT피셜 : Unity의 렌더링 시스템과 최적화 메커니즘으로 인해 실제 동작은 다소 복잡하고, 예상과 다를 수 있습니다
        // 라고 하기는 하는데 잘 모르겠다..
        //_enemyRenderer.sharedMaterial = _spawnMat; 
        #endregion
        Color tempColor = _spawnMat.color;
        tempColor.a = 0;
        _spawnMat.color = tempColor;

        for (int i = 0; i < _enemyRenderer.Length; i++)
        {
            _enemyRenderer[i].material = _spawnMat;

            if (i == _enemyRenderer.Length - 1)
                _enemyRenderer[i].material.DOFade(1, 1).OnComplete(SpawnComplete);
            else
                _enemyRenderer[i].material.DOFade(1, 1);
        }
    }

    void SpawnComplete()
    {
        //_enemyRenderer.sharedMaterial = _baseMat;
        foreach (var renderer in _enemyRenderer)
        {
            renderer.material = _baseMat;
        }
        SpawnEnd();

        Init();
    }

    void SpawnEnd()
    {
        IsSpawning = false;
    }

    void InvokeOnDieFadeOut()
    {
        Invoke(nameof(OnDieFadeOut), 1.5f);
    }

    void OnDieFadeOut()
    {
        Color tempColor = _spawnMat.color;
        tempColor.a = 1;
        _spawnMat.color = tempColor;


        for (int i = 0; i < _enemyRenderer.Length; i++)
        {
            _enemyRenderer[i].material = _spawnMat;

            if (i == _enemyRenderer.Length - 1)
                _enemyRenderer[i].material.DOFade(1, 1).OnComplete(ActiveFalse);
            else
                _enemyRenderer[i].material.DOFade(1, 1);
        }
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
