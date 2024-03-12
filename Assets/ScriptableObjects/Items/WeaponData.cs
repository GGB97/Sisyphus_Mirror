using UnityEngine;

public enum WeaponType
{
    MeleeAttack,
    RangeAttack
}

public abstract class WeaponData : ItemSO
{
    public float Atk => _atk;
    public float AtkRate => _atkRate;
    public float CritRate => _critRate;
    public float CritDamage => _critDamage;
    public float Range => _range; 
    public int LifeSteal => _lifeSteal;
    public WeaponType Type => _type;
    public int WeaponTier => _weaponTier;

    [field: Header("Weapon")]
    [SerializeField] float _atk;
    [SerializeField] float _atkRate;
    [SerializeField] float _critRate;
    [SerializeField] float _critDamage;
    [SerializeField] float _range;
    [SerializeField] int _lifeSteal;
    [SerializeField] protected WeaponType _type;
    [SerializeField] int _weaponTier;
}
