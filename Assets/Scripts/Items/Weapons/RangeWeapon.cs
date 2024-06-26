using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponPivot;
    [SerializeField] private Transform _weaponContainer;

    public List<Transform> Target = new List<Transform>();
    Transform currentTarget;

    [SerializeField] private Animator _animator;

    [SerializeField] private int id;
    public WeaponData weaponData;

    private float _coolDown;
    public bool canAttack;

    protected Vector3 direction;

    void Start()
    {
        _animator = GetComponent<Animator>();

        _weaponContainer = transform.parent;
        transform.position = GetRandomPosition();

        _coolDown = 0f;
        canAttack = true;
    }

    public void Init(int id)
    {
        weaponData = DataBase.Weapon.Get(id);
        this.id = id;
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-1.5f, -1f);
        float z = Random.Range(-1.5f, -1f);

        return new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    }

    private void Update()
    {
        _coolDown -= Time.deltaTime;

        if (_coolDown <= 0 && canAttack && DungeonManager.Instance.gameState == DungeonState.Playing)
        {
            canAttack = false;
            Target.Clear();

            DetectEnemyInRange();

            _coolDown = weaponData.AtkSpeed;
        }
    }

    public void DetectEnemyInRange()
    {
        Target.Clear();

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, weaponData.Range, 1 << 7);
        if (colliders.Length == 0)
        {
            canAttack = true;
            return;
        }

        foreach (Collider collider in colliders)
        {
            Target.Add(collider.transform);
        }

        int random = Random.Range(0, colliders.Length);
        if (Target[random].GetComponent<Enemy>().isDie || Target[random].GetComponent<Enemy>().IsSpawning)
        {
            Target.RemoveAt(random);
            Target.Clear();
            canAttack = true;

            return;
        }

        currentTarget = Target[random];

        RotateWeapon(currentTarget.position);
    }

    private void RotateWeapon(Vector3 target)
    {
        Vector3 dir = target - transform.position;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;
        SoundManager.Instance.PlayAudioClip(weaponData.SfxTag);
        Attack();
    }

    void Attack()
    {
        _animator.SetTrigger("Attack");     // 추후에 수정
        _animator.SetFloat("AttackSpeed", 1 + weaponData.AtkSpeed);
        Shot();

        canAttack = true;
    }

    void Shot()
    {
        GameObject _go = ObjectPoolManager.Instance.SpawnFromPool((int)weaponData.ProjectileID, _weaponPivot.position, _weaponPivot.rotation);
        ProjectileTest _projectile = _go.GetComponent<ProjectileTest>();
        if (_projectile.sfxTag != null) SoundManager.Instance.PlayAudioClip(_projectile.sfxTag);
        SetProjectileTarget(_projectile);
        _projectile.AddExcludeLayer(LayerData.Player);
        _projectile.AddExcludeLayer(LayerMask.NameToLayer("Default"));

        float value = SetAttackDamage();
        _projectile.SetValue(value);
        _projectile.SetVelocity(1f); // 속도 배율 설정

        if (weaponData.NumberOfProjectile > 1)
        {
            int projectilePerShot = weaponData.NumberOfProjectile;

            for (int i = 1; i < projectilePerShot; ++i)
            {
                float randomRot = Random.Range(_weaponPivot.rotation.y - .1f, _weaponPivot.rotation.y + .1f);
                Quaternion rot = _weaponPivot.rotation;
                rot.y = randomRot;
                GameObject go = ObjectPoolManager.Instance.SpawnFromPool((int)weaponData.ProjectileID, _weaponPivot.position, rot);

                ProjectileTest projectile = go.GetComponent<ProjectileTest>();
                SetProjectileTarget(projectile);

                projectile.SetValue(value);
                projectile.SetVelocity(1f); // 속도 배율 설정
            }
        }
    }

    void SetProjectileTarget(ProjectileTest projectile)
    {
        if (projectile is Projectile_Guided)
        {
            projectile.AddTarget(LayerData.Enemy, currentTarget);
        }
        else
        {
            projectile.AddTarget(LayerData.Enemy);
        }
    }

    public float SetAttackDamage()
    {
        float damage = 0;
        Status _player = GameManager.Instance.Player.currentStat;

        if (weaponData.PhysicalAtk != 0)
            damage = weaponData.PhysicalAtk + _player.physicalAtk;
        else
            damage = weaponData.MagicAtk + _player.magicAtk;

        damage /= weaponData.NumberOfProjectile;

        float random = UnityEngine.Random.Range(1, 101);
        if (_player.critRate > random) damage += (damage * _player.critDamage / 100);

        return Mathf.Ceil(damage);
    }

    public int GetWeaponId()
    {
        return id;
    }

    public Vector3 GetWeaponPivot()
    {
        return _weaponPivot.position;
    }
}
