using UnityEngine;

public class Weapon_Wand_1_Epic : Weapon_Epic
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _skillRate = 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Skill()
    {
        Wand_1_Epic_Skill skill = ParticleObjectPool.Instance.SpawnFromPool(_weaponID, _target.transform.position, Quaternion.identity).GetComponent<Wand_1_Epic_Skill>();
        skill.OnSkillStart();

        _isSkillAvailable = false;
        _timeSinceLastSkill = Time.time;

        _target = null;
        Target.Clear();
    }
}
