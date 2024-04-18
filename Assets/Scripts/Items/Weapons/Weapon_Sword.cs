using UnityEngine;

public class Weapon_Sword : MonoBehaviour
{
    float rotSpeed = 500;
    bool _isAttack = false;

    [SerializeField] MeleeWeapon _weapon;

    private void Update()
    {
        //if (_isAttack)
        //{
        //    Vector3 dir = transform.position - _weapon.targetPos;
        //    dir.y = 0;
        //    if (dir != Vector3.zero)
        //    {
        //        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        //        transform.rotation = rot;
        //    }
        //}
    }

    public void AttackAnimation()
    {
        _isAttack = true;
        _weapon.OnAnimationStart();
    }

    public void AttackEnd()
    {
        _isAttack = false;
        _weapon.OnAnimationEnd();
        transform.rotation = Quaternion.Euler(-180, 0, 0);
    }
}
