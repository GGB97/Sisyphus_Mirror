using System.Text;
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
    [SerializeField] float _physicalAtk;
    [SerializeField] float _magicAtk;
    [SerializeField] float _def;
    [SerializeField] float _atkSpeed;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _critRate;
    [SerializeField] float _critDamage;
    [SerializeField] float _lifeSteal;

    public EquipmentType EquipmentType => _type;
    public float Health => _health;
    public float PhysicalAtk => _physicalAtk;
    public float MagicAtk => _magicAtk;
    public float Def => _def;
    public float AtkSpeed => _atkSpeed;
    public float MoveSpeed => _moveSpeed;
    public float CritRate => _critRate;
    public float CritDamage => _critDamage;
    public float LifeSteal => _lifeSteal;
    public override StringBuilder SetExplantion(ItemSO itemSO)
    {
        StringBuilder sb = base.SetExplantion(itemSO);
        if (Health != 0)
        {
            Utilities.AddText(sb, "체력", Health);
        }
        if (PhysicalAtk != 0)
        {
            Utilities.AddText(sb, "물리 공격", PhysicalAtk);
        }
        if (MagicAtk != 0)
        {
            Utilities.AddText(sb, "마법 공격", MagicAtk);
        }
        if (Def != 0)
        {
            Utilities.AddText(sb, "방어력", Def);
        }
        if (AtkSpeed != 0)
        {
            Utilities.AddText(sb, "공격 속도", AtkSpeed, true);
        }
        if (MoveSpeed != 0)
        {
            Utilities.AddText(sb, "이동 속도", MoveSpeed, true);
        }
        if (CritRate != 0)
        {
            Utilities.AddText(sb, "치명타 확률", CritRate, true);
        }
        if (CritDamage != 0)
        {
            Utilities.AddText(sb, "치명타 데미지", CritDamage);
        }
        if (LifeSteal != 0)
        {
            Utilities.AddText(sb, "피해 흡혈", LifeSteal, true);
        }

        return sb;
    }
}
