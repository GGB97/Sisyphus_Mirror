using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill_Base : MonoBehaviour
{
    protected GameManager _gm;
    protected DungeonManager _dm;

    protected Player _player;

    protected bool _isCooldown;
    protected float _cooldown;
    [SerializeField] protected float _currentCooldown;

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

    protected void CoolDownInit()
    {
        _isCooldown = false;
        _currentCooldown = _cooldown;
    }

    protected virtual void SubEvent()
    {
        _player.Input.PlayerActions.Skill.started += UseSkill;
    }

    protected virtual void UnSubEvent()
    {
        _player.Input.PlayerActions.Skill.started -= UseSkill;
    }
}
