using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AttackStart()
    {
        enemy.AttackStart(0);
    }

    public void AttackEnd()
    {
        enemy.AttackEnd(0);
    }

    public void Shoot()
    {
        enemy.RangedAttack(0, 0);
    }
}
