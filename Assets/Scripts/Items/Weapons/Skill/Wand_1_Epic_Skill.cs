using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_1_Epic_Skill : MonoBehaviour
{
    [SerializeField] ParticleSystem _skillEffect;
    [SerializeField] ParticleSFX _skillSFX;

    string _sfxTag = "Wand_1_Epic_Skill";
    float _coolTime = 3.5f;
    float _skillStartTime;
    bool _skillStart;
    float _damage;

    void Update()
    {
        if(_skillStart && Time.time - _skillStartTime >= _coolTime) gameObject.SetActive(false);
    }

    public void OnSkillStart()
    {
        _skillEffect.Play();
        _skillStartTime = Time.time;
        _skillStart = true;
        _skillSFX.PlaySFX(_sfxTag);
        _damage = Mathf.Floor(50 + (0.5f * DungeonManager.Instance.currnetstage));
    }

    private void OnTriggerStay(Collider other)
    {
        if(LayerData.Enemy == (1 << other.gameObject.layer | LayerData.Enemy))
        {
            if(other.TryGetComponent<HealthSystem>(out HealthSystem enemy))
            {
                if (!other.GetComponent<Enemy>().IsSpawning)
                    enemy.TakeDamage(_damage, DamageType.Magic);
            }
        }
    }

    private void OnDisable()
    {
        ParticleObjectPool.Instance.ReturnToPull(gameObject);
    }
}
