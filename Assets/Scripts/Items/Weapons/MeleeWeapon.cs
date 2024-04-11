using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    [SerializeField] private Animator _animator;
    [SerializeField] GameObject _effect;

    public List<Transform> Target = new List<Transform>();
    public Transform _weaponPivot;
    Vector3 _weaponPosY = new Vector3(0, 1.5f, 0);
    Vector3 _weaponPos;

    [SerializeField] private int id;
    [SerializeField] Vector3 _targetPos;

    private WeaponIdleAnimation _idleAnimation;

    bool _isMoving;
    bool _canAttack;
    bool _animationEnd = true;

    float _timeStartedMoving;

    private void Start()
    {
        if (transform.parent == null) Destroy(gameObject);

        _animator = GetComponent<Animator>();
        _idleAnimation = GetComponent<WeaponIdleAnimation>();

        _weaponData = DataBase.Weapon.Get(id);
        _weaponPivot = transform.parent;

        _weaponPos = GetRandomPosition();
        transform.localPosition = _weaponPos;

        _effect.SetActive(false);
        _isMoving = false;
        _canAttack = true;
        _idleAnimation.isFloating = true;
    }

    private void Update()
    {
        if(Target.Count == 0 && _canAttack && DungeonManager.Instance.gameState == GameState.Playing)
        {
            DetectEnemyInRange();
        }
    }

    private void FixedUpdate()
    {
        // 선형 보간 시작한 시간
        float timeSinceStarted = Time.time - _timeStartedMoving;
        // 선형 보간 진행 정도
        float percentageComplete = timeSinceStarted / (_weaponData.AtkSpeed / 3);

        if (Target.Count != 0 && _isMoving && _canAttack)
        {
            // 무기 방향 회전
            Vector3 dir = transform.position - _targetPos;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = rot;

            dir = _targetPos - transform.position;
            dir.y = 0;
            //_targetPos = new Vector3(transform.position.x + dir.normalized.x, transform.position.y + dir.normalized.y, transform.position.z + dir.normalized.z);
            _targetPos = new Vector3(_targetPos.x - (0.1f * dir.normalized.x), _targetPos.y - (0.1f * dir.normalized.y), _targetPos.z - (0.1f * dir.normalized.z));

            // 근접 공격 이동
            transform.position = Vector3.Lerp(transform.position, _targetPos, percentageComplete);
            
            // 이동이 완료되면
            //if(percentageComplete >= .5f)
            //{
            //    _animationEnd = false;
            //    //Debug.Log("Melee Attack");
            //    // 공격 애니메이션 재생
            //    _animator.SetBool("Attack", true);
            //    _animator.SetFloat("AttackSpeed", 1 + _weaponData.AtkSpeed);
            //}
            if (percentageComplete >= 1f)
            {
                //_effect.SetActive(true);
                //_timeStartedMoving = Time.time;
                //_canAttack = false;
                _animationEnd = false;
                //Debug.Log("Melee Attack");
                // 공격 애니메이션 재생
                _animator.SetBool("Attack", true);
                _animator.SetFloat("AttackSpeed", 1 + _weaponData.AtkSpeed);
            }
        }
        else if(_animationEnd && !_canAttack)
        {
            transform.position = Vector3.Lerp(transform.position, _weaponPivot.position, percentageComplete);

            if (percentageComplete >= 1)
            {
                transform.localPosition = _weaponPos;

                Target.Clear();
                Invoke("SetCanAttack", _weaponData.AtkSpeed / 3);
                _idleAnimation.isFloating = true;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        return new Vector3(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z + z);
    }

    void SetCanAttack()
    {
        _canAttack = true;
    }

    public void DetectEnemyInRange()
    {
        Target.Clear();

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, _weaponData.Range, LayerData.Enemy);
        if (colliders.Length == 0) return;

        foreach (Collider collider in colliders)
        {
            //Debug.Log($"Detect : {collider.name}");
            Target.Add(collider.transform);
        }

        int random = Random.Range(0, colliders.Length);
        if (Target[random].GetComponent<Enemy>().isDie && Target[random].GetComponent<Enemy>().IsSpawning)
        {
            Target.Clear();
            return;
        }
        _targetPos = Target[random].position;

        // TODO : Monster의 크기에 맞게 공격 위치 변경해주기... 가능하다면
        _targetPos.y = (Vector3.up * 1.0f).y;


        transform.parent = null;
        _idleAnimation.isFloating = false;
        _timeStartedMoving = Time.time;
        _isMoving = true;
    }

    void OnAnimationStart()
    {
        _effect.SetActive(true);
        _timeStartedMoving = Time.time;
        _canAttack = false;
    }

    void OnAnimationEnd()
    {
        _effect.GetComponent<TrailRenderer>().Clear();
        _effect.SetActive(false);
        transform.parent = _weaponPivot;
        _animator.SetBool("Attack", false);
        _animationEnd = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Enemy == (1 << other.gameObject.layer | LayerData.Enemy))
        {
            //Debug.Log("Monster TakeDamage");
            //HealthSystem _healthSystem = other.gameObject.GetComponent<HealthSystem>();
            //_healthSystem.TakeDamage(_weaponData.PhysicalAtk);
            if(other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem _healthSystem))
            {
                _healthSystem.TakeDamage(_weaponData.PhysicalAtk, DamageType.Physical);
            }
        }
        else
        {
            //Debug.Log($"Trigger failure {LayerMask.NameToLayer("Enemy")}");
            return;
        }
    }
}
