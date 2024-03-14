using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponPivot;

    private Transform _target;

    public List<Transform> Target = new List<Transform>();

    [SerializeField] private Animator _animator;

    public int id;
    public WeaponData weaponData;

    public float timer;
    public bool canAttack;

    protected Vector3 direction;

    void Start()
    {
        _weaponPivot = GetComponentInChildren<Transform>();
        id = 20001011;  // 추후에 수정
        weaponData = DataBase.Weapon.Get(id);
        _animator = GetComponent<Animator>();

        timer = 0f;
        canAttack = true;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && canAttack)
        {
            canAttack = false;
            DetectEnemyInRange();

            timer = weaponData.AtkRate;
        }
    }

    public void DetectEnemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, weaponData.Range, 1 << 7);

        foreach (Collider collider in colliders)
        {
            Debug.Log($"Detect : {collider.name}");
            Target.Add(collider.transform);
        }

        RotateWeapon(Target[0].position);
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
        Debug.Log("attack");

        Shot();

        _animator.SetTrigger("Attack");     // 추후에 수정
        _animator.SetFloat("AttackSpeed", 1 + weaponData.AtkRate);

        canAttack = true;
    }

    void Shot()
    {
        ObjectPoolManager.Instance.SpawnFromPool("Arrow", _weaponPivot.position, _weaponPivot.rotation);

        if (weaponData.NumberOfProjectile > 1)
        {
            int projectilePerShot = weaponData.NumberOfProjectile;

            for (int i = 1; i < projectilePerShot; ++i)
            {
                float randomRot = Random.Range(_weaponPivot.rotation.y - .1f, _weaponPivot.rotation.y + .1f);
                Quaternion rot = _weaponPivot.rotation;
                rot.y = randomRot;
                ObjectPoolManager.Instance.SpawnFromPool("Arrow", _weaponPivot.position, rot);
            }
        }
    }
}
