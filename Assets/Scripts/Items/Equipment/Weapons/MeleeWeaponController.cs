using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    [SerializeField] MeleeWeaponData meleeWeaponData;
    [SerializeField] MeleeWeaponBase meleeWeaponBase;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //meleeWeaponBase = (MeleeWeaponBase)meleeWeaponData.CreateItem();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            meleeWeaponBase.Attack();
            timer = meleeWeaponData.atkRate;
        }
    }
}
