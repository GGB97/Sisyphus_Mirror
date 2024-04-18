using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Ricochet : ProjectileTest
{
    List<Transform> targets;
    int _ricochetCount;

    protected override void OnHit()
    {
        if (_ricochetCount <= 0)
        {
            base.OnHit();
            return;
        }

        targets.Clear();

        // 범위 내 적 탐지

        // 적 선택

        // 방향 계산

        --_ricochetCount;
    }
}
