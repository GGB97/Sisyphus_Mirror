using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Guided : ProjectileTest
{
    Transform targetPos;

    float chasingDelay;
    [SerializeField] float chasingInterval = 0.05f; // 회전을 할 간격

    Vector3 directionToTarget;

    protected override void Init()
    {
        base.Init();

        chasingDelay = 10;
        targetPos = null;
    }

    protected override void DurationCheck()
    {
        base.DurationCheck();

        chasingDelay += Time.deltaTime;
    }

    protected override void MoveToTarget()
    {
        // 여기서 추적
        if (chasingDelay >= 0.05f)
        {
            //방향 조절
            LookTargetSlerp();

            chasingDelay = 0;
        }

        _rb.velocity = gameObject.transform.forward * (_data.speed * _velocity);
        //base.MoveToTarget();
    }

    protected Quaternion LookTargetPos() // 바라볼 방향 계산
    {
        Vector3 directionToLookAt = targetPos.position - transform.position;
        directionToLookAt.y = 0; // 수평 회전만 고려

        return Quaternion.LookRotation(directionToLookAt);
    }

    protected void LookTargetSlerp() // 대상을 천천히 바라봄
    {
        Quaternion targetRotation = LookTargetPos();

        // 바라보는 방향 수정
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
    }

    public override void AddTarget(LayerMask layer, Transform target)
    {
        base.AddTarget(layer, target);
        targetPos = target;
    }
}
