public class RangeWeaponBase : WeaponBase
{
    public RangeWeaponData RangeWeaponData { get; private set; }

    public RangeWeaponBase(RangeWeaponData data) : base(data)
    {
        RangeWeaponData = data;
    }

    public override void Equip()
    {
        // TODO : 플레이어에게 스탯 반영하기
    }

    public override void UnEquip()
    {
        // TODO : 플레이어에게서 스탯 회수
    }

    public override void Attack()
    {
        // TODO : 공격 애니메이션 재생, 공격 처리
        
    }
}
