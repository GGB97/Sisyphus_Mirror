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
    public override StringBuilder SetExplantion(ItemSO itemSO)
    {
        StringBuilder sb = base.SetExplantion(itemSO);
        if (Health != 0)
        {
            Utilities.AddText(sb, nameof(Health), Health);
        }
        if (MeleeAtk != 0)
        {
            Utilities.AddText(sb, nameof(MeleeAtk), MeleeAtk);
        }
        if (MagicAtk != 0)
        {
            Utilities.AddText(sb, nameof(MagicAtk), MagicAtk);
        }
        if (Def != 0)
        {
            Utilities.AddText(sb, nameof(Def), Def);
        }
        if (AtkSpeed != 0)
        {
            Utilities.AddText(sb, nameof(AtkSpeed), AtkSpeed);
        }
        if (MoveSpeed != 0)
        {
            Utilities.AddText(sb, nameof(MoveSpeed), MoveSpeed);
        }
        if (CritRate != 0)
        {
            Utilities.AddText(sb, nameof(CritRate), CritRate);
        }
        if (CritDamage != 0)
        {
            Utilities.AddText(sb, nameof(CritDamage), CritDamage);
        }
        if (LifeSteal != 0)
        {
            Utilities.AddText(sb, nameof(LifeSteal), LifeSteal);
        }

        return sb;
    }
}
