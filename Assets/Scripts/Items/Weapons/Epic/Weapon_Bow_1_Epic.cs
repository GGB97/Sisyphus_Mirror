using UnityEngine;

public class Weapon_Bow_1_Epic : Weapon_Epic
{
    Vector3 _firePivot;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _skillRate = 2;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _isSkillAvailable = false;
    }

    protected override void Skill()
    {
        _firePivot = _rangeWeapon.GetWeaponPivot();
        _firePivot.y = 0.5f;

        for (int i = 0; i < 8; ++i)
        {
            Quaternion rot = Quaternion.Euler(0, 45 * i, 0);
            GameObject arrow = ObjectPoolManager.Instance.SpawnFromPool(40000002, _firePivot, rot);

            ProjectileTest _projectile = arrow.GetComponent<ProjectileTest>();
            if (_projectile.sfxTag != null) SoundManager.Instance.PlayAudioClip(_projectile.sfxTag);
            
            _projectile.AddTarget(LayerData.Enemy);
            _projectile.AddExcludeLayer(LayerData.Player);
            _projectile.AddExcludeLayer(LayerMask.NameToLayer("Default"));

            _projectile.SetValue(_rangeWeapon.SetAttackDamage());
            _projectile.SetVelocity(1f);
        }

        _timeSinceLastSkill = Time.time;
        Target.Clear();
    }
}
