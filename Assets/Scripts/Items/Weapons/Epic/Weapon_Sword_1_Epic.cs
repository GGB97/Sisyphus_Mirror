using Unity.VisualScripting;
using UnityEngine;

public class Weapon_Sword_1_Epic : Weapon_Epic
{
    Player _player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _skillRate = 5;

        _player = GameManager.Instance.Player;
    }

    protected override void Skill()
    {
        if (_weapon.Target.Count == 0) return;
        Vector3 rot = Vector3.zero;

        int random = Random.Range(0, _weapon.Target.Count);
        rot = _weapon.Target[random].transform.position - _player.transform.position;
        rot.y = 0;

        Sword_1_Epic_Skill skill = ParticleObjectPool.Instance.SpawnFromPool(_weaponID, transform.position, Quaternion.LookRotation(rot)).GetComponent<Sword_1_Epic_Skill>();
        skill.OnSkillStart();
    }
}
