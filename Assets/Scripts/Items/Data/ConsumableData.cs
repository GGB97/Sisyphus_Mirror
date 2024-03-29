using System.Text;
using UnityEngine;

[System.Serializable]
public class ConsumableData : ItemSO
{
    [field: Header("Consumable")]
    [SerializeField] int _health;
    [SerializeField] float _meleeAtk;
    [SerializeField] float _magicAtk;
    [SerializeField] float _def;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _stageDuration;   // 효과의 지속 시간. 효과가 지속될 스테이지 수를 의미.

    public int Health => _health;
    public float MeleeAtk => _meleeAtk;
    public float MagicAtk => _magicAtk;
    public float Def => _def;
    public float AttackSpeed => _attackSpeed;
    public float MoveSpeed => _moveSpeed;
    public int StageDuration => _stageDuration;
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
        if (AttackSpeed != 0)
        {
            Utilities.AddText(sb, nameof(AttackSpeed), AttackSpeed);
        }
        if (MoveSpeed != 0)
        {
            Utilities.AddText(sb, nameof(MoveSpeed), MoveSpeed);
        }
        if (MoveSpeed != 0)
        {
            Utilities.AddText(sb, nameof(MoveSpeed), MoveSpeed);
        }

        return sb;
    }
}
