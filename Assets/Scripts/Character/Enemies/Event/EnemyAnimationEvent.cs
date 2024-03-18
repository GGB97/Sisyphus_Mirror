using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] GameObject AttackPivot;
    Collider attackCollider;
    Transform attackPos;
    Enemy enemy;

    [SerializeField] GameObject testPrefab;

    private void Awake()
    {
        attackCollider = AttackPivot.GetComponent<Collider>();
        attackPos = AttackPivot.transform;
        enemy = GetComponentInParent<Enemy>();
    }

    public void AttackStart()
    {
        attackCollider.enabled = true;
        //Debug.Log("Attack Start");
    }

    public void AttackEnd()
    {
        attackCollider.enabled = false;
        //Debug.Log("Attack End");
    }

    public void Shoot()
    {
        GameObject go = Instantiate(testPrefab, attackPos.position, Quaternion.identity); // 이걸 오브젝트풀에서 가져오게 하면될듯

        Vector3 directionToTarget = enemy.target.position - enemy.transform.position;
        directionToTarget.y = 0f;
        directionToTarget.Normalize();

        go.GetComponent<Rigidbody>().AddForce(directionToTarget * 8f, ForceMode.Impulse);
    }
}
