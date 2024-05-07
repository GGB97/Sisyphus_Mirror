using System.Collections.Generic;
using UnityEngine;

public class Projectile_Ricochet : ProjectileTest
{
    List<Collider> searchTargets = new(30); // 30도 큰거같긴 함.
    Collider[] searchTargetsArray = new Collider[30];
    float searchRange = 4f;

    int _ricochetCount;

    protected override void Init()
    {
        base.Init();
        _ricochetCount = _data.count;
    }

    protected override void OnHit(LayerMask layer)
    {
        if (_ricochetCount <= 0)
        {
            base.OnHit(layer);
            return;
        }

        // 범위 내 적 탐지
        SearchInRange();

        // 적 선택
        Collider col = SelectTarget();

        // 방향 설정
        SetDirection(col);

        --_ricochetCount;
    }

    void SearchInRange()
    {
        int size = Physics.OverlapSphereNonAlloc(
            gameObject.transform.position, searchRange, searchTargetsArray, 
            _target &= ~LayerData.Terrain, QueryTriggerInteraction.Ignore);

        searchTargets.Clear();
        for (int i = 0; i < size; i++)
        {
            searchTargets.Add(searchTargetsArray[i]);
        }
    }

    Collider SelectTarget()
    {
        // 유효한 적들만 있는 배열에서 랜덤을 뽑아내는 방식은 타겟의 수가 많을 때 더 효율적일 수 있음
        // 현재 게임에서는 한번에 탐지한 적의 수가 30 이내로 예상되기 때문에 아래의 방법을 채택

        int rand = 0;
        while (searchTargets.Count > 0)
        {
            rand = Random.Range(0, searchTargets.Count);
            int id = searchTargets[rand].GetComponent<CharacterBehaviour>().GetActiveID();
            if (attackedTarget.ContainsKey(id))
            {
                searchTargets.RemoveAt(rand);
            }
            else // 공격한적이 없는 타겟일 경우 반복문 종료
            {
                break;
            }
        }

        if (searchTargets.Count == 0)
        {
            // 중복되지 않은 적을 찾지 못함.
            return null;
        }

        return searchTargets[rand];
    }

    void SetDirection(Collider col) // 방향 설정
    {
        if (col != null)
        {
            // null이 아니면 적을 탐지 했다는거니까 해당 방향으로 전환
            transform.rotation = LookTargetPos(col.transform);
        }
        else
        {
            // 그냥 무작위 방향?
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            randomDirection.y = 0; // 수평 회전만 고려
            transform.rotation = Quaternion.LookRotation(randomDirection);
        }
    }

    protected Quaternion LookTargetPos(Transform target) // 바라볼 방향 계산
    {
        Vector3 directionToLookAt = target.position - transform.position;
        directionToLookAt.y = 0; // 수평 회전만 고려

        return Quaternion.LookRotation(directionToLookAt);
    }
}
