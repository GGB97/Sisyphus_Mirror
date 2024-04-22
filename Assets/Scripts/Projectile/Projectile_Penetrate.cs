using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Penetrate : ProjectileTest
{
    int _penetrateCount;

    protected override void Init()
    {
        base.Init();
        _penetrateCount = _data.count;
    }

    protected override void OnHit(LayerMask layer)
    {
        LayerMask terrain = LayerData.Terrain;
        if (_penetrateCount <= 0 || 1 << layer.value == terrain)
        {
            base.OnHit(layer);
            return;
        }

        //방향 설정
        Quaternion rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.localRotation = rot;

        --_penetrateCount;
    }
}
