using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public int id;
    protected ProjectileData _data;

    [SerializeField] protected LayerMask _target; // 부딪혀서 데미지를 줘야하는 하는 대상 Layer

    public ParticleSystem ps;
    protected Collider _projectileCollider;
    protected Rigidbody _rb;

    [SerializeField] protected GameObject hitParticle;

    protected float _value; // 데미지
    protected float _velocity;
    protected float _duration;

    public DamageType GetDamageType => _data.type;
    public string sfxTag => _data.sfxTag;

    private void Awake()
    {
        _projectileCollider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();

        _data = DataBase.Projectile.Get(id);

        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        hitParticle.SetActive(false);
        _projectileCollider.enabled = true;

        _target = LayerData.Terrain; // target 초기화
        _projectileCollider.includeLayers = LayerData.Terrain; // 기본적으로 벽/바닥에는 부딪히고 사라져야 하니까
        _projectileCollider.excludeLayers = LayerData.Projectile; // 투사체간의 충돌로 지워지지 않게 하기 위해 초기값으로

        _duration = _data.duration;
    }

    private void OnDisable()
    {
        //CancelInvoke(); // invoke가 Disable 하는거라 들어올거같진 않지만
        ObjectPoolManager.Instance.ReturnToPull(gameObject);
        hitParticle.SetActive(false);
    }

    protected virtual void Update()
    {
        DurationCheck();

        MoveToTarget();
    }

    protected virtual void DurationCheck()
    {
        _duration -= Time.deltaTime;
        if (_duration <= 0)
        {
            // TODO : Object Pooling
            gameObject.SetActive(false);
        }
    }

    protected virtual void MoveToTarget()
    {
        // 속도가 점점 느려지게 하려면 AddForce의 ForceMode.Impulse를 사용하거나 _velodity를 조건을 통해 점점 낮추면 될듯
        _rb.velocity = gameObject.transform.forward * (_data.speed * _velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        int hitLayer = 1 << other.gameObject.layer;
        bool isContained = (hitLayer & _target) != 0; // 현재 충돌한 객체가 target에 포함이 되는지
        if (isContained)
        {
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem _healthSystem))
            {
                _healthSystem.TakeDamage(_value, _data.type);
            }
        }
        else return;
        OnHit(); // 일단 ExcludeLayer가 아니니까 들어온 이상 사라져야함
    }

    void OnHit()
    {
        _projectileCollider.enabled = false;
        //rb.velocity = Vector3.zero;
        _velocity = 0f;

        ps?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        hitParticle?.SetActive(true);

        Invoke(nameof(ActiveFalse), 1f);
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public virtual void AddTarget(LayerMask layer) // 부딪히고 조건검사 해야할 Layer 추가
    {
        _target |= layer;
        _projectileCollider.includeLayers |= _target;
    }

    public virtual void AddTarget(LayerMask layer, Transform target) // 부딪히고 조건검사 해야할 Layer 추가
    {
        AddTarget(layer);
    }

    public virtual void AddExcludeLayer(LayerMask layer) // 부딪히지 않아야할 Layer 추가
    {
        _projectileCollider.excludeLayers |= layer;
    }

    public void SetValue(float value)
    {
        _value = value;
    }

    public void SetVelocity(float vel)
    {
        _velocity = vel;
    }
}
