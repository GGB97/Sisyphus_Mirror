using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Axe_1_Epic : Weapon_Epic
{
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
        Debug.Log($"Weapon Skill {_weaponID}");
        Axe_1_Epic_Skill skill = ParticleObjectPool.Instance.SpawnFromPool(_weaponID, _target.transform.position, Quaternion.identity).GetComponent<Axe_1_Epic_Skill>();
        skill.OnSkillStart();

        _isSkillAvailable = false;
        _timeSinceLastSkill = Time.time;

        _target = null;
        Target.Clear();
    }
}
