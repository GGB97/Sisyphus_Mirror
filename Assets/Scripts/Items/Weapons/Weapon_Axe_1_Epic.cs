using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_Axe_1_Epic : MonoBehaviour
{
    [SerializeField] MeleeWeapon _weapon;
    public List<Enemy> Target = new List<Enemy>();
    Enemy _target;

    float _skillRate = 5;
    float _timeSinceLastSkill = 0;
    bool _isSkillAvailable;

    int _weaponID;

    // Start is called before the first frame update
    void Start()
    {
        _weaponID = _weapon.GetWeaponId();
        _timeSinceLastSkill = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _timeSinceLastSkill >= _skillRate) _isSkillAvailable = true;

        if (Target.Count == 0 && DungeonManager.Instance.gameState == DungeonState.Playing && _isSkillAvailable)
        {
            DetectEnemyInRange();
        }
    }

    void Skill()
    {
        Debug.Log($"Weapon Skill {_weaponID}");
        Axe_1_Epic_Skill skill = ParticleObjectPool.Instance.SpawnFromPool(_weaponID, _target.transform.position, Quaternion.identity).GetComponent<Axe_1_Epic_Skill>();
        skill.OnSkillStart();

        _isSkillAvailable = false;
        _timeSinceLastSkill = Time.time;
        Target.Clear();
    }

    public void DetectEnemyInRange()
    {
        Target.Clear();

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 10, LayerData.Enemy);
        if (colliders.Length == 0) return;

        foreach (Collider collider in colliders)
        {
            Target.Add(collider.GetComponent<Enemy>());
        }

        int random = Random.Range(0, colliders.Length);
        if (Target[random].isDie || Target[random].IsSpawning)
        {
            Target.Clear();
            return;
        }
        _target = Target[random];

        Skill();
    }
}
