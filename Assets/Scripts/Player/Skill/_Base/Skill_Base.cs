using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Skill_Base : MonoBehaviour
{
    protected GameManager _gm;
    protected DungeonManager _dm;
    protected Player _player;

    [Header("Skill")]
    [SerializeField] public Sprite icon;
    [SerializeField] protected ParticleSystem effect_Ps;

    [SerializeField] protected bool _isCooldown;
    protected float _cooldown;
    [SerializeField] protected float _currentCooldown;

    [SerializeField] protected bool _isActive;
    protected float _duration = 6f;
    [SerializeField] protected float _currentduration;

    public Action<bool, bool, float, float> cooldownUpdate;

    private void Awake()
    {
        _player = GetComponent<Player>();

        _gm = GameManager.Instance;
        if (_gm.gameState == GameState.Dungeon)
            _dm = DungeonManager.Instance;

        Init();
    }

    private void OnEnable()
    {
        if (_dm != null)
            SubEvent();
    }

    private void OnDisable()
    {
        if (_dm != null)
            UnSubEvent();
    }

    protected virtual void Init()
    {

    }

    private void Update()
    {
        //if (_isCooldown) // 쿨타임이 돌때만 호출하게 하려면 UpdateUI의 아래 조건을 활성화 해야함.
        {
            if (_isActive)
                cooldownUpdate?.Invoke(_isCooldown, _isActive, _duration, _currentduration);
            else
                cooldownUpdate?.Invoke(_isCooldown, _isActive, _cooldown, _currentCooldown);
        }
    }

    protected virtual void UseSkill(InputAction.CallbackContext context)
    {
        if (_dm.gameState != DungeonState.Playing)
        {
            Debug.Log("Is not Playing");
            return;
        }

        if (_isCooldown == false)
        {
            Skill();
        }
    }

    public virtual void Skill()
    {
        StartCoroutine(nameof(ActiveSkill));
        StartCoroutine(nameof(CoolDown));
    }

    protected IEnumerator ActiveSkill()
    {
        _isActive = true;

        if (effect_Ps != null)
            effect_Ps.Play();

        SetBuff();

        while (_currentduration > 0)
        {
            _currentduration -= Time.deltaTime;

            yield return null;
        }

        if (_currentduration < 0)
        {
            _currentduration = 0;

            ResetBuff();

            effect_Ps.Stop();
            effect_Ps.Clear();

            ActiveInfoInit();
        }
    }

    protected virtual void SetBuff()
    {
        // 적용할 버프
    }

    protected virtual void ResetBuff()
    {
        // 버프 해제
    }

    protected IEnumerator CoolDown()
    {
        _isCooldown = true;

        while (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;

            yield return null;
        }

        if (_currentCooldown <= 0)
        {
            CoolDownInit();
        }
    }
    protected void ActiveInfoInit()
    {
        _isActive = false;
        _currentduration = _duration;
    }

    protected void CoolDownInit()
    {
        _isCooldown = false;
        _currentCooldown = _cooldown;
    }

    protected virtual void SubEvent()
    {
        _player.Input.PlayerActions.Skill.started += UseSkill;

        _dm.OnStageClear += StopSkill;
    }

    protected virtual void UnSubEvent()
    {
        _player.Input.PlayerActions.Skill.started -= UseSkill;

        _dm.OnStageClear -= StopSkill;
    }

    protected virtual void StopSkill()
    {
        StopCoroutine(nameof(ActiveSkill));
        effect_Ps.Stop();
        effect_Ps.Clear();
        ActiveInfoInit();

        StopCoroutine(nameof(CoolDown));
        CoolDownInit();
    }
}
