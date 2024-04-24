using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_Epic : MonoBehaviour
{
    [SerializeField] protected MeleeWeapon _weapon;
    public List<Enemy> Target = new List<Enemy>();
    protected Enemy _target;

    protected float _skillRate = 5;
    protected float _timeSinceLastSkill = 0;
    protected bool _isSkillAvailable;

    protected int _weaponID;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _weaponID = _weapon.GetWeaponId();
        _timeSinceLastSkill = Time.time;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Time.time - _timeSinceLastSkill >= _skillRate) _isSkillAvailable = true;

        if (Target.Count == 0 && DungeonManager.Instance.gameState == DungeonState.Playing && _isSkillAvailable)
        {
            DetectEnemyInRange();
        }
    }

    void DetectEnemyInRange()
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

    protected abstract void Skill();
}
