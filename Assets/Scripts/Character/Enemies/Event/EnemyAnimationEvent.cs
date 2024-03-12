using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] Collider attackCollider;

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
}
