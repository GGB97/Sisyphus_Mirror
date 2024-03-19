using UnityEngine;

public class Equipments : MonoBehaviour, IEquipable
{
    [SerializeField] int _id;
    [SerializeField] EquipmentsData _data;

    public void OnEquip()
    {
        // TODO : 장착 시 플레이어 스탯 반영
    }

    public void OnUnequip()
    {
        // TODO : 장비 해제 시 플레이어 스탯 반영
    }
}
