using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Adrenaline : Skill_Base
{
    [Header("Skill 1")]
    [SerializeField] ParticleSystem skill01_Ps;

    bool skill01_IsCooldown;
    float skill01_Cooldown = 20f;
    [SerializeField] float skill01_CurrentCooldown;

    bool skill01_IsActive;
    float skill01_duration = 6f;
    [SerializeField] float skill01_Currentduration;

    protected override void Init()
    {
        skill01_IsCooldown = false;
        skill01_Cooldown = 10f;
        skill01_CurrentCooldown = skill01_Cooldown;

        skill01_IsActive = false;
        skill01_duration = 6f;
        skill01_Currentduration = skill01_duration;
    }

    protected override void SubEvent()
    {
        _player.Input.PlayerActions.Skill.started += UseSkill;

        _dm.OnStageClear += StopSkill;
    }

    protected override void UnSubEvent()
    {
        _player.Input.PlayerActions.Skill.started -= UseSkill;

        _dm.OnStageClear -= StopSkill;
    }

    protected override void UseSkill(InputAction.CallbackContext context)
    {
        if (_dm.gameState != DungeonState.Playing)
        {
            Debug.Log("Is not Playing");
            return;
        }

        if (skill01_IsCooldown == false)
        {
            Skill();
        }
    }

    public override void Skill()
    {
        StartCoroutine(nameof(ActiveSkill01));
        StartCoroutine(nameof(CoolDown));
    }

    IEnumerator ActiveSkill01()
    {
        skill01_IsActive = true;

        if (skill01_Ps != null)
            skill01_Ps.Play();

        _player.currentStat.critRate += 20;

        while (skill01_Currentduration > 0)
        {
            skill01_Currentduration -= Time.deltaTime;

            yield return null;
        }

        if (skill01_Currentduration < 0)
        {
            skill01_Currentduration = 0;

            _player.currentStat.critRate -= 20;

            skill01_Ps.Stop();
            skill01_Ps.Clear();

            ActiveInfoInit();
        }
    }

    void StopSkill()
    {
        StopCoroutine(nameof(ActiveSkill01));
        skill01_Ps.Stop();
        skill01_Ps.Clear();
        ActiveInfoInit();

        StopCoroutine(nameof(CoolDown));
        CoolDownInit();
    }

    void CoolDownInit()
    {
        skill01_IsCooldown = false;
        skill01_CurrentCooldown = skill01_Cooldown;
    }

    void ActiveInfoInit()
    {
        skill01_IsActive = false;
        skill01_Currentduration = skill01_duration;
    }

    IEnumerator CoolDown()
    {
        skill01_IsCooldown = true;

        while (skill01_CurrentCooldown > 0)
        {
            skill01_CurrentCooldown -= Time.deltaTime;

            yield return null;
        }

        if(skill01_CurrentCooldown <= 0)
        {
            CoolDownInit();
        }
    }
}
