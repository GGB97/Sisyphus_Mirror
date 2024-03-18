using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : CharacterBehaviour
{
    public EnemyStateMachine stateMachine;
    public Transform target;

    public float chasingDelay;
    public float attackDelay;

    [field: SerializeField] public EnemyInfo Info { get; private set; }
    public Status modifier; // 스탯 가중치 (Player의 경우 장비에 의한 가중치, Enemy의 경우 난이도/층수 에 의한 가중치)

    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public CharacterController Controller { get; private set; } // 아직은 안쓰고있음. 나중에도 안쓰면 제거 예정

    // 나중에 기본 속도와 추가값에 비례해서 리턴하도록 프로퍼티로 수정하면 될듯
    public float animAttackSpeed = 1f;
    public float animMoveSpeed = 1f;

    [SerializeField] Collider[] _meleeAttackColliders;

    [SerializeField] Transform[] _rangeAttackPos;
    [SerializeField] GameObject[] _projectilePrefabs; 

    private void Awake()
    {
        Init();
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

    void Init()
    {
        Info = DataBase.EnemyStats.Get(id);
        currentStat.InitStatus(Info, modifier);

        Animator = GetComponentInChildren<Animator>();
        #region AnimatorOverrideController 으로 시도했던것
        // OverrideAnimator는 속도 조절에는 사용하지 않아도 되지만 시도해본 방법중 하나였음.
        // OverrideAnimator는 애니메이션 클립을 부분적으로만 변경을 하려 할 때 사용하기 좋을 것 같음
        // ex)특정 조건 만족시 공격 모션이나 그런게 변할 때?
        //OverrideAnimator = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        //Animator.runtimeAnimatorController = OverrideAnimator;
        #endregion
        Animator.SetFloat(EnemyAnimationData.AttackSpeedParameterHash, animAttackSpeed); // 각 객체의 속도별로 Animation의 속도를 조절하기 위해서
        Animator.SetFloat(EnemyAnimationData.MoveSpeedParameterHash, animMoveSpeed); // 현재 프로젝트에서는 필요 없을 것 같지만 그냥 해보고싶었음.

        Agent = GetComponent<NavMeshAgent>();
        Controller = GetComponent<CharacterController>();

        stateMachine = new(this);

        Agent.stoppingDistance = Info.attackRange - .2f; // 사거리보다 살짝 더 들어가게끔 하지 않으면 멈춰서 이상한짓함
        Agent.angularSpeed = 240f;

        isDie = false;
        OnDieEvent += ChangeDieState;

        isHit = false;
        OnHitEvent += ChangeHitState;

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

    public void OnChildTriggerEnter(Collider other)
    {
        //이곳에서 자식 콜라이더의 트리거 충돌 처리
        Debug.Log($"OnChildTriggerEnter : {gameObject.name} -> Attack : {other.gameObject.name}");
    }

    void ChangeDieState()
    {
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

    public void RangedAttack(int prfabNum, int posNum)
    {
        GameObject go = Instantiate(_projectilePrefabs[prfabNum], 
            _rangeAttackPos[posNum].transform.position, transform.rotation); // 이걸 오브젝트풀에서 가져오게 하면될듯

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0f;
        directionToTarget.Normalize();

        ProjectileTest projectile = go.GetComponent<ProjectileTest>();

        projectile.AddTarget(LayerData.Player);
        projectile.AddExcludeLayer(LayerData.Enemy);
        projectile.rb.AddForce(directionToTarget * 10f, ForceMode.Impulse);
    }
}
