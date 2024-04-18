using UnityEngine;

public class WeaponAttackEvent : MonoBehaviour
{
    bool _isAttack = false;

    [SerializeField] MeleeWeapon _weapon;

    public void AttackAnimation()
    {
        _isAttack = true;
        _weapon.OnAnimationStart();
    }

    public void AttackEnd()
    {
        _isAttack = false;
        _weapon.OnAnimationEnd();
        //transform.rotation = Quaternion.Euler(-180, 0, 0);
    }
}
