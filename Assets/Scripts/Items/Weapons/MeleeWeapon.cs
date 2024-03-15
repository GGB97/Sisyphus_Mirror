using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    [SerializeField] private Animator _animator;
    [SerializeField] private TrailRenderer _trailRenderer;

    public List<Transform> Target = new List<Transform>();

    private int id;
    [SerializeField] private float _coolDown;
    private bool _canAttack;

    private void Start()
    {
        id = 10001011;
        _animator = GetComponent<Animator>();
        _weaponData = DataBase.Weapon.Get(id);

        _canAttack = true;
    }

    private void Update()
    {
        _coolDown -= Time.deltaTime;

        if(_coolDown <= 0 && _canAttack)
        {
            _trailRenderer.Clear();

            _canAttack = false;
            // TODO : 공격
            Debug.Log("Melee Attack");
            DetectEnemyInRange();

            _coolDown = _weaponData.AtkRate;
        }
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

        int random = Random.Range(0, colliders.Length);
        Attack(Target[random].position);
    }

    private void Attack(Vector3 position)
    {
        Vector3 currentPosition = transform.position;

        StartCoroutine(Move(currentPosition, position));
        //transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
        Debug.Log("Melee Attack!");
        _animator.SetTrigger("Attack");

        //Vector3.Lerp(transform.position, currentPosition, Time.deltaTime / 2);
        _canAttack = true;
    }

    IEnumerator Move(Vector3 startPos, Vector3 destination)
    {
        while (startPos.Equals(destination))
        {
            transform.position = Vector3.Lerp(transform.position, destination, 0.05f);
        }
        yield return new WaitUntil(() => startPos.Equals(destination));
    }
}
