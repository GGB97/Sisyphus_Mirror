using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    public float atkSpeed;

    Player _player;
    Status _playerStatus;

    [SerializeField] private Animator _animator;
    [SerializeField] Collider _collider;
    [SerializeField] GameObject _effect;

    public List<Enemy> Target = new List<Enemy>();
    public Transform _weaponPivot;
    Vector3 _weaponPosY = new Vector3(0, 1.5f, 0);
    Vector3 _weaponPos;

    [SerializeField] private int id;
    [SerializeField] public Enemy _target;

    private WeaponIdleAnimation _idleAnimation;

    bool _isMoving;
    bool _canAttack;
    bool _animationEnd = true;

    float _timeStartedMoving;

    private void Start()
    {
        if (transform.parent == null) Destroy(gameObject);

        _collider.enabled = false;

        _idleAnimation = GetComponent<WeaponIdleAnimation>();

        //atkSpeed = _weaponData.AtkSpeed;
        _weaponPivot = transform.parent;

        _weaponPos = GetRandomPosition();
        transform.localPosition = _weaponPos;

        _effect.SetActive(false);
        _isMoving = false;
        _canAttack = true;
        _idleAnimation.isFloating = true;

        _player = GameManager.Instance.Player;
        _playerStatus = _player.currentStat;

        atkSpeed += 1 + ((1 - _weaponData.AtkSpeed) * 2 + (_player.currentStat.attackSpeed / 200));
    }

    public void Init(int id)
    {
        _weaponData = DataBase.Weapon.Get(id);
    }

    private void Update()
    {
        if (Target.Count == 0 && _canAttack && DungeonManager.Instance.gameState == DungeonState.Playing)
        {
            DetectEnemyInRange();
        }
    }

    private void FixedUpdate()
    {
        // 선형 보간 시작한 시간
        float timeSinceStarted = Time.time - _timeStartedMoving;
        // 선형 보간 진행 정도
        //float percentageComplete = timeSinceStarted / (_weaponData.AtkSpeed / 3);
        float percentageComplete = timeSinceStarted / (atkSpeed);

        if (Target.Count != 0 && _isMoving && _canAttack)
        {
            _animationEnd = false;

            // 무기 방향 회전
            //Vector3 dir = transform.position - targetPos;
            Vector3 dir = _target.transform.position - transform.position;
            dir.y = 0;

            transform.rotation = Quaternion.LookRotation(dir);

            dir = _target.transform.position - transform.position;
            dir.y = 0;

            Vector3 targetPos = new Vector3(_target.transform.position.x - (0.3f * dir.normalized.x), _target.transform.position.y + .5f, _target.transform.position.z - (0.3f * dir.normalized.z));

            // 근접 공격 이동
            //transform.position = Vector3.Lerp(transform.position, targetPos, percentageComplete);
            transform.position = Vector3.Lerp(transform.position, targetPos, atkSpeed / 5);

            // 이동이 완료되면
            if (Mathf.Abs(transform.position.x - targetPos.x) < .1f && Mathf.Abs(transform.position.z - targetPos.z) < .1f)
            {
                _animationEnd = false;
                _canAttack = false;

                _animator.SetBool("Attack", true);
                _animator.SetFloat("AttackSpeed", atkSpeed);
            }
        }
        else if (_animationEnd && !_canAttack)
        {
            transform.rotation = Quaternion.identity;

            transform.position = Vector3.Lerp(transform.position, _weaponPivot.position, atkSpeed / 5);

            if (Mathf.Abs(transform.position.x - _weaponPivot.position.x) < .1f && Mathf.Abs(transform.position.y - _weaponPivot.position.y) < .1f && Mathf.Abs(transform.position.z - _weaponPivot.position.z) < .1f)
            {
                transform.localPosition = _weaponPos;

                Target.Clear();
                //Invoke("SetCanAttack", atkSpeed);
                _canAttack = true;
                _idleAnimation.isFloating = true;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-1.5f, -1f);
        float z = Random.Range(-1.5f, -1f);

        return new Vector3(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z + z);
    }

    void SetCanAttack()
    {
        _canAttack = true;
    }

    public void DetectEnemyInRange()
    {
        Target.Clear();

        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _weaponData.Range, LayerData.Enemy);
        if (colliders.Length == 0) return;

        foreach (Collider collider in colliders)
        {
            Target.Add(collider.GetComponent<Enemy>());
        }

        int random = Random.Range(0, colliders.Length);
        if (Target[random].isDie || Target[random].IsSpawning)
        {
            Target.Clear();
            return;
        }
        _target = Target[random];

        // TODO : Monster의 크기에 맞게 공격 위치 변경해주기... 가능하다면

        transform.parent = null;
        _idleAnimation.isFloating = false;
        _timeStartedMoving = Time.time;
        _isMoving = true;
    }

    public void OnAnimationStart()
    {
        _effect.SetActive(true);
        _collider.enabled = true;

        SoundManager.Instance.PlayAudioClip(_weaponData.SfxTag);
        _timeStartedMoving = Time.time;
    }

    public void OnAnimationEnd()
    {
        _collider.enabled = false;
        _effect.GetComponent<TrailRenderer>().Clear();
        _effect.SetActive(false);
        transform.rotation = Quaternion.Euler(-180, 0, 0);
        transform.parent = _weaponPivot;
        _animator.SetBool("Attack", false);
        _animationEnd = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Enemy == (1 << other.gameObject.layer | LayerData.Enemy))
        {
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem _healthSystem))
            {
                //_hit.Play();
                GameObject go = ParticleObjectPool.Instance.SpawnFromPool(_weaponData.ParticleID, other.gameObject.transform.position, Quaternion.identity);
                go.GetComponent<Particle>().PlayerPartcle();

                _healthSystem.TakeDamage(SetAttackDamage(), _weaponData.PhysicalAtk != 0 ? DamageType.Physical : DamageType.Magic);
                //Invoke("ToggleParticle", atkSpeed);

                if (_weaponData.LifeSteal != 0)
                {
                    int random = Random.Range(0, 101);
                    if (_playerStatus.lifeSteal >= random)
                    {
                        _playerStatus.health++;
                        _player.HealthChange();
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    //void ToggleParticle()
    //{
    //    _hit.Stop();
    //}

    private float SetAttackDamage()
    {
        float damage = _weaponData.PhysicalAtk != 0 ? _weaponData.PhysicalAtk : _weaponData.MagicAtk;

        if (_weaponData.PhysicalAtk != 0)
            damage += _playerStatus.physicalAtk;
        else damage += _playerStatus.magicAtk;

        float random = UnityEngine.Random.Range(1, 101);
        if (_playerStatus.critRate > random) damage += (damage * _playerStatus.critDamage / 100);

        return Mathf.Ceil(damage);
    }

    public int GetWeaponId()
    {
        return id;
    }
}
