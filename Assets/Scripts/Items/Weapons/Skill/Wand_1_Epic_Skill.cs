using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Wand_1_Epic_Skill : MonoBehaviour
{
    [SerializeField] ParticleSystem _skillEffect;
    [SerializeField] ParticleSFX _skillSFX;

    string _sfxTag = "Wand_1_Epic_Skill";
    float _coolTime = 3.5f;
    float _skillStartTime;
    bool _skillStart;
    float _damage;

    float _hitDelay = 0.5f;
    bool _canHit = true;

    void Update()
    {
        if(_skillStart && Time.time - _skillStartTime >= _coolTime) gameObject.SetActive(false);
        if(Time.time - _hitDelay >= 0.5f)
        {
            _canHit = true;
            CheckMonsterInRange();
        }
    }

    public void OnSkillStart()
    {
        _skillEffect.Play();
        _skillStartTime = Time.time;
        _skillStart = true;
        _skillSFX.PlaySFX(_sfxTag);
        _damage = Mathf.Floor(50 + (0.5f * DungeonManager.Instance.currnetstage));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

    void CheckMonsterInRange()
    {
        if (!_canHit) return;

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 5, LayerData.Enemy);
        if (colliders.Length == 0) return;

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<HealthSystem>(out HealthSystem _enemy))
            {
                _enemy.TakeDamage(_damage, DamageType.Magic);
                _canHit = false;
                _hitDelay = Time.time;
            }
        }
    }

    private void OnDisable()
    {
        ParticleObjectPool.Instance.ReturnToPull(gameObject);
    }
}
