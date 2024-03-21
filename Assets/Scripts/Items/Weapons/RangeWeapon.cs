using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponPivot;
    [SerializeField] private Transform _weaponContainer;

    public List<Transform> Target = new List<Transform>();

    [SerializeField] private Animator _animator;

    [SerializeField] private int id;
    public WeaponData weaponData;

    private float _coolDown;
    public bool canAttack;

    protected Vector3 direction;

    void Start()
    {
        weaponData = DataBase.Weapon.Get(id);
        _animator = GetComponent<Animator>();
        _weaponContainer = transform.parent;

        _coolDown = 0f;
        canAttack = true;
    }

    private void Update()
    {
        _coolDown -= Time.deltaTime;

        if (_coolDown <= 0 && canAttack)
        {
            Debug.Log("Range Attack");
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
            Debug.Log($"Detect : {collider.name}");
            Target.Add(collider.transform);
        }

        int random = Random.Range(0, colliders.Length);
        RotateWeapon(Target[random].position);
    }

    private void RotateWeapon(Vector3 target)
    {
        Vector3 dir = target - transform.position;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;
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
        _projectile.AddTarget(LayerData.Enemy);
        _projectile.AddExcludeLayer(LayerData.Player);
        _projectile.AddExcludeLayer(LayerMask.NameToLayer("Default"));

        float value = _projectile.GetDamageType == DamageType.Physical ? weaponData.PhysicalAtk : weaponData.MagicAtk;
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
                GameObject go = ObjectPoolManager.Instance.SpawnFromPool((int)ProjectileID.Arrow, _weaponPivot.position, rot);

                ProjectileTest projectile = go.GetComponent<ProjectileTest>();
                projectile.AddTarget(LayerData.Enemy);

                projectile.SetValue(value);
                projectile.SetVelocity(1f); // 속도 배율 설정
            }
        }
    }
}
