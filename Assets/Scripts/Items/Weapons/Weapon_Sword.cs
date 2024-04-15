using UnityEngine;

public class Weapon_Sword : MonoBehaviour
{
    float rotSpeed = 500;
    float atkSpeed;
    bool _isAttack = false;

    [SerializeField] MeleeWeapon _weapon;

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
            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir.normalized);
                transform.rotation = rot;
            }
        }
    }

    public void AttackAnimation()
    {
        _isAttack = true;
    }

    public void AttackEnd()
    {
        _isAttack = false;
        transform.rotation = Quaternion.Euler(-180, 0, 0);
    }
}
