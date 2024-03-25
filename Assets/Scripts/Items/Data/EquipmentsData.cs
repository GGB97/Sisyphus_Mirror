using UnityEngine;

public enum EquipmentType
{
    Armor,
    Accessories
}

[System.Serializable]
public class EquipmentsData : ItemSO
{
    [field: Header("Stats")]
    [SerializeField] EquipmentType _type;
    [SerializeField] float _health;
    [SerializeField] float _meleeAtk;
    [SerializeField] float _magicAtk;
    [SerializeField] float _def;
    [SerializeField] float _atkSpeed;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _critRate;
    [SerializeField] float _critDamage;
    [SerializeField] float _lifeSteal;

    public EquipmentType EquipmentType => _type;
    public float Health => _health;
    public float MeleeAtk => _meleeAtk;
    public float MagicAtk => _magicAtk;
    public float Def => _def;
    public float AtkSpeed => _atkSpeed;
    public float MoveSpeed => _moveSpeed;
    public float CritRate => _critRate;
    public float CritDamage => _critDamage;
    public float LifeSteal => _lifeSteal;
}
