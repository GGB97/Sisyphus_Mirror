using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public LayerMask target;

    public ParticleSystem ps;
    [HideInInspector] public Collider projectileCollider;
    [HideInInspector] public Rigidbody rb;

    [SerializeField] GameObject hitParticle;

    private void Awake()
    {
        projectileCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        hitParticle.SetActive(false);
        projectileCollider.enabled = true;

        target = LayerData.Test; // 초기화
        projectileCollider.includeLayers = 0;
        projectileCollider.excludeLayers = LayerData.Projectile;
    }

    private void OnDisable()
    {
        hitParticle.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        int hitLayer = 1 << other.gameObject.layer;
        bool isContained = (hitLayer & target) != 0; // 현재 충돌한 객체가 target에 포함이 되는지
        if (isContained)
        {
            Debug.Log($"OnTriggerEnter : {other.gameObject.name}");
        }
        OnHit(); // 일단 ExcludeLayer가 아니니까 들어온 이상 사라져야함
    }

    void OnHit()
    {
        projectileCollider.enabled = false;
        rb.velocity = Vector3.zero;

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        hitParticle.SetActive(true);

        Invoke(nameof(ActiveFalse), 1f);
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public void AddTarget(LayerMask layer) // 부딪히고 조건검사 해야할 Layer 추가
    {
        target |= layer;
        projectileCollider.includeLayers |= target;
    }

    public void AddExcludeLayer(LayerMask layer) // 부딪히지 않아야할 Layer 추가
    {
        projectileCollider.excludeLayers |= layer;
    }
}
