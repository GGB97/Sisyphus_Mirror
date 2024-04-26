using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public int id;
    ProjectileData _data;

    [SerializeField] LayerMask _target;

    [SerializeField] protected GameObject attackParticle;
    [SerializeField] protected GameObject range;
    [SerializeField] protected Transform fill;

    public float fillDuration;
    public float attackRange;
    protected float _value;

    protected Collider[] colliders;

    public DamageType GetDamageType => _data.type;

    protected void Awake()
    {
        colliders = new Collider[3];

        _data = DataBase.Projectile.Get(id);
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
        _target = 0;
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

        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, attackRange, colliders, _target);

        for (int i = 0; i < numColliders; i++)
        {
            // 여기서 공격
            HealthSystem hs = colliders[i].GetComponent<HealthSystem>();
            hs.TakeDamage(_value * coefficient, _data.type);
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
        _target |= layer;
    }

    public void SetValue(float value)
    {
        _value = value;
    }
}
