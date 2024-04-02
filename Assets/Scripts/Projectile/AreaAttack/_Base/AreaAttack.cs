using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class AreaAttack : MonoBehaviour
{
    [SerializeField] DamageType damageType;
    public LayerMask target;

    [SerializeField] protected GameObject attackParticle;
    [SerializeField] protected GameObject range;
    [SerializeField] protected Transform fill;

    public float fillDuration;
    public float attackRange;
    protected float _value;

    protected Collider[] colliders;

    public DamageType GetDamageType => damageType;

    protected void Awake()
    {
        colliders = new Collider[3];
    }

    protected void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        ObjectPoolManager.Instance.ReturnToPull(gameObject);
    }

    protected void Init()
    {
        target = 0;
        fill.transform.localScale = Vector3.zero;
    }

    public void AttackStart()
    {
        fill.transform.DOScale(Vector3.one, fillDuration).OnComplete(Attack);
    }

    protected virtual void Attack()
    {
        range.SetActive(false);

        attackParticle.SetActive(true);

        // 상속받는 AreaAttack_xxx 에서 HitCheck 해야함
    }

    protected virtual void HitCheck(float coefficient)
    {
        Clear();

        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, attackRange, colliders, target);

        for (int i = 0; i < numColliders; i++)
        {
            // 여기서 공격
            HealthSystem hs = colliders[i].GetComponent<HealthSystem>();
            hs.TakeDamage(_value * coefficient);
        }
    }

    protected IEnumerator DelayedHitCheck(float delayTime, float coefficient)
    {
        // delayTime만큼 대기
        yield return new WaitForSeconds(delayTime);

        // HitCheck 함수 호출
        HitCheck(coefficient);
    }

    protected void Clear()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = null;
        }
    }

    public void AddTarget(LayerMask layer) // 부딪히고 조건검사 해야할 Layer 추가
    {
        target |= layer;
    }

    public void SetValue(float value)
    {
        _value = value;
    }
}
