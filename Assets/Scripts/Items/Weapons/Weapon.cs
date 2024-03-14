using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Transform> Target = new List<Transform>();
    [SerializeField] private Animator _animator;

    public int id;
    public WeaponData weaponData;

    public float timer;
    public bool canAttack;

    public bool isAttackStart;

    protected Vector3 direction;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        id = 20001011;  // 추후에 수정
        weaponData = DataBase.Weapon.Get(id);
        _animator = GetComponent<Animator>();

        //timer = weaponData.AtkRate;
        canAttack = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && canAttack)
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

        isAttackStart = true;
    }

    protected virtual void Attack()
    {
        _animator.SetTrigger("Attack");     // 추후에 수정
    }
}
