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

    public void AttackStart(int index)
    {
        enemy.AttackStart(index);
    }

    public void AttackEnd(int index)
    {
        enemy.AttackEnd(index);
    }

    public void Shoot(int index)
    {
        enemy.RangedAttack(index);
    }

    public void AreaAttack(int index)
    {
        enemy.AreaAttack(index);
    }
}
