using UnityEngine;

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
}
