using UnityEngine;

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
            float x = rot.x < 0 ? -1 : 1;
            float z = rot.z < 0 ? -1 : 1;

            transform.Rotate(90 * x, rot.y, rot.z);

            transform.position = Vector3.Lerp(transform.position, dir, moveSpeed * Time.deltaTime);
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
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
