using System.Text;
using UnityEngine;

[System.Serializable]
public class ConsumableData : ItemSO
{
    [field: Header("Consumable")]
    [SerializeField] int _health;
    [SerializeField] float _physicalAtk;
    [SerializeField] float _magicAtk;
    [SerializeField] float _def;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _stageDuration;   // 효과의 지속 시간. 효과가 지속될 스테이지 수를 의미.

    public int Health => _health;
    public float PhysicalAtk => _physicalAtk;
    public float MagicAtk => _magicAtk;
    public float Def => _def;
    public float AttackSpeed => _attackSpeed;
    public float MoveSpeed => _moveSpeed;
    public int StageDuration => _stageDuration;

    public int SetDuration()
    {
        return _stageDuration--;
    }

    public override StringBuilder SetExplantion(ItemSO itemSO)
    {
        StringBuilder sb = base.SetExplantion(itemSO);
        if (Health != 0)
        {
            Utilities.AddText(sb, "체력", Health);
        }
        if (_physicalAtk != 0)
        {
            Utilities.AddText(sb, "물리 공격력", _physicalAtk);
        }
        if (MagicAtk != 0)
        {
            Utilities.AddText(sb, "마법 공격력", MagicAtk);
        }
        if (Def != 0)
        {
            Utilities.AddText(sb, "방어력", Def);
        }
        if (AttackSpeed != 0)
        {
            Utilities.AddText(sb, "공격 속도", AttackSpeed, true);
        }
        if (MoveSpeed != 0)
        {
            Utilities.AddText(sb, "이동 속도", MoveSpeed, true);
        }
        if (StageDuration != 0)
        {
            Utilities.AddText(sb, "지속 스테이지", StageDuration);
        }

        return sb;
    }
}
