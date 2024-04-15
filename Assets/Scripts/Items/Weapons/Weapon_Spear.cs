using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Spear : MonoBehaviour
{
    float moveSpeed = 0.5f;
    bool _isAttack = false;

    [SerializeField] MeleeWeapon _weapon;
    Vector3 _positon;

    private void Start()
    {
        _weapon = GetComponent<MeleeWeapon>();
    }

    private void Update()
    {
        if (_isAttack)
        {
            Vector3 dir = transform.position - _weapon.targetPos;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = rot;

            transform.Translate(dir * moveSpeed);
        }
    }

    public void AttackAnimation()
    {
        _positon = transform.position;
        _isAttack = true;
    }

    public void AttackEnd()
    {
        _isAttack = false;
        transform.position = _positon;
        transform.rotation = Quaternion.Euler(-180, 0, 0);
    }
}
