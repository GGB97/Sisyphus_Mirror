using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int _damage; // 추후에 Player에게서 현재 공격력 가져오기
    float _duration;  // 추후에 Const 설정하기
    Rigidbody _rigidbody;
    TrailRenderer _trailRenderer;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _duration = 5f;
    }

    private void Update()
    {
        _duration -= Time.deltaTime;
        if(_duration <= 0)
        {
            // TODO : Object Pooling
            gameObject.SetActive(false);
        }
        // TODO : 발사체 이동 처리
        _rigidbody.velocity = gameObject.transform.forward * 5;     // 추후 수정
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1 << 7)
        {
            Debug.Log("Monster TakeDamage");
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        _trailRenderer.Clear();
        ObjectPoolManager.Instance.ReturnToPull(gameObject);
    }
}
