using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill_Base : MonoBehaviour
{
    protected GameManager _gm;
    protected DungeonManager _dm;

    protected Player _player;

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

    }

    public virtual void Skill()
    {

    }

    protected virtual void SubEvent()
    {

    }

    protected virtual void UnSubEvent()
    {

    }
}
