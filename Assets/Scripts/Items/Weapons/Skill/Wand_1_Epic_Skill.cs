using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_1_Epic_Skill : MonoBehaviour
{
    [SerializeField] ParticleSystem _skillEffect;
    [SerializeField] ParticleSFX _skillSFX;

    float _coolTime = 3.5f;
    float _skillStartTime;
    bool _skillStart;

    void Update()
    {
        if(_skillStart && Time.time - _skillStartTime >= _coolTime) gameObject.SetActive(false);
    }

    public void OnSkillStart()
    {
        _skillEffect.Play();
        _skillStartTime = Time.time;
        _skillStart = true;
        _skillSFX.PlaySFX();
    }

    private void OnTriggerStay(Collider other)
    {
        if(LayerData.Enemy == (1 << other.gameObject.layer | LayerData.Enemy))
        {
            if(other.TryGetComponent<HealthSystem>(out HealthSystem enemy))
            {
                if (!other.GetComponent<Enemy>().IsSpawning)
                    enemy.TakeDamage(50, DamageType.Magic);
            }
        }
    }

    private void OnDisable()
    {
        ParticleObjectPool.Instance.ReturnToPull(gameObject);
    }
}
