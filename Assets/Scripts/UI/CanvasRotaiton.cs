using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotaiton : MonoBehaviour
{
    Camera main;

    private void Awake()
    {
        main = Camera.main;
    }

    void FixedUpdate()
    {
        LookTarget();
    }

    protected Quaternion LookTargetPos() // 바라볼 방향 계산
    {
        Vector3 directionToLookAt = transform.position - main.transform.position;
        directionToLookAt.y = 0;

        return Quaternion.LookRotation(directionToLookAt);
    }

    void LookTarget() // 대상을 즉시 바라봄
    {
        Quaternion targetRotation = LookTargetPos();

        // 바라보는 방향 수정
        transform.rotation = targetRotation;
    }
}
