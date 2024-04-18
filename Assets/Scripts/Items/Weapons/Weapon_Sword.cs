using DG.Tweening;
using UnityEngine;

public class Weapon_Sword : MonoBehaviour
{
    float rotSpeed = 500;
    float atkSpeed = 1.5f;
    bool _isAttack = false;

    [SerializeField] MeleeWeapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<MeleeWeapon>();
        atkSpeed = _weapon.atkSpeed;
    }

    private void FixedUpdate()
    {
        if (_isAttack)
        {
            Debug.Log("is Attack");
            transform.Rotate(5, 0, 360 / 20 * atkSpeed);
        }
    }

    public void AttackAnimation()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        _isAttack = true;
    }

    public void AttackEnd()
    {
        _isAttack = false;
        transform.rotation = Quaternion.Euler(-180, 0, 0);
    }
}
