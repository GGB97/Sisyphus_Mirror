using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sword : MonoBehaviour
{
    float rotSpeed = 500f;
    bool _isAttack = false;

    [SerializeField] MeleeWeapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<MeleeWeapon>();
    }

    private void Update()
    {
        if (_isAttack)
            transform.Rotate(new Vector3(transform.rotation.x + rotSpeed * Time.deltaTime, transform.rotation.y + rotSpeed * Time.deltaTime, transform.rotation.z));
    }

    public void AttackAnimation()
    {
        Vector3 dir = transform.position - _weapon.targetPos;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;

        if (transform.rotation.x < 0) rotSpeed = -rotSpeed;

        _isAttack = true;
    }

    public void AttackEnd()
    {
        _isAttack = false;
        transform.rotation = Quaternion.Euler(-180, 0, 0);
    }
}
