using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Adrenaline : Skill_Base
{
    [Header("Skill")]
    [SerializeField] ParticleSystem effect_Ps;

    bool _isActive;
    float _duration = 6f;
    [SerializeField] float _currentduration;

    protected override void Init()
    {
        _isCooldown = false;
        _cooldown = 10f;
        _currentCooldown = _cooldown;

        _isActive = false;
        _duration = 6f;
        _currentduration = _duration;
    }

    protected override void SubEvent()
    {
        base.SubEvent();

        _dm.OnStageClear += StopSkill;
    }

    protected override void UnSubEvent()
    {
        base.UnSubEvent();

        _dm.OnStageClear -= StopSkill;
    }

    public override void Skill()
    {
        StartCoroutine(nameof(ActiveSkill));
        StartCoroutine(nameof(CoolDown));
    }

    IEnumerator ActiveSkill()
    {
        _isActive = true;

        if (effect_Ps != null)
            effect_Ps.Play();

        _player.currentStat.critRate += 20;

        while (_currentduration > 0)
        {
            _currentduration -= Time.deltaTime;

            yield return null;
        }

        if (_currentduration < 0)
        {
            _currentduration = 0;

            _player.currentStat.critRate -= 20;

            effect_Ps.Stop();
            effect_Ps.Clear();

            ActiveInfoInit();
        }
    }

    void StopSkill()
    {
        StopCoroutine(nameof(ActiveSkill));
        effect_Ps.Stop();
        effect_Ps.Clear();
        ActiveInfoInit();

        StopCoroutine(nameof(CoolDown));
        CoolDownInit();
    }

    void ActiveInfoInit()
    {
        _isActive = false;
        _currentduration = _duration;
    }
}
