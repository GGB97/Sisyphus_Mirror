using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    [SerializeField] private Animator _animator;
    [SerializeField] private TrailRenderer _trailRenderer;

    public List<Transform> Target = new List<Transform>();

    [SerializeField] private int id;
    [SerializeField] private float _coolDown;
    private bool _canAttack;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weaponData = DataBase.Weapon.Get(id);

        StartCoroutine("Attack");

        _canAttack = true;
    }

    private void Update()
    {
        
    }

    public void DetectEnemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, _weaponData.Range, 1 << 7);
        if (colliders.Length == 0) return;

        foreach (Collider collider in colliders)
        {
            Debug.Log($"Detect : {collider.name}");
            Target.Add(collider.transform);
        }
        //Attack(Target[random].position);
    }

    //private void Attack(Vector3 position)
    //{
    //    _canAttack = false;
    //    Vector3 currentPosition = transform.position;

    //    //StartCoroutine(Move(currentPosition, position));
    //    //transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
    //    Debug.Log("Melee Attack!");
    //    _animator.SetTrigger("Attack");

    //    //Vector3.Lerp(transform.position, currentPosition, Time.deltaTime / 2);
    //    _canAttack = true;
    //}

    IEnumerator Attack()
    {
        while (true)
        {
            _trailRenderer.enabled = true;
            DetectEnemyInRange();

            if (Target.Count == 0) continue;

            int random = Random.Range(0, Target.Count);
            Vector3 currentPosition = transform.position;
            Move(Target[random].position);

            _animator.SetTrigger("Attack");
            _trailRenderer.enabled = false;

            Move(currentPosition);
            yield return new WaitForSeconds(_weaponData.AtkRate);
        }
    }

    private void Move(Vector3 position)
    {
        while (!transform.position.Equals(position))
        {
            transform.Translate(position);
        }
    }
}
