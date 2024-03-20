using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void OnEquip();
    void OnUnequip();
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public Transform PlayerTransform {  get; private set; }
    public Player Player { get; private set; }
    [SerializeField] PlayerBaseData _playerStats;
    public Dictionary<ItemType, List<InventoryItem>> ownItems;
    private Dictionary<int, WeaponData> _ownWeapons;
    private Dictionary<int, EquipmentsData> _ownEquipments;
    private Dictionary<int, ConsumableData> _ownConsumable;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player = PlayerTransform.GetComponent<Player>();
        _playerStats = Player.Data;
    }

    public void UpdateItemList()
    {
        ownItems.Clear();
        // ownItems = InventoryController.Instance.인벤토리아이템
        // 인벤토리에서 넘어온 아이템 리스트 가지고 Itemtype에 따라 분류?
        // foreach(WeaponData weapon in ownItems[ItemType.Weapon]) 이런 식으로 하면 될듯?
    }

    public void OnEquip(int id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                _ownWeapons.Add(id, DataBase.Weapon.Get(id));
                break;
            case ItemType.Consumable:
                _ownConsumable.Add(id, DataBase.Consumable.Get(id));
                break;
            case ItemType.Equipments:
                _ownEquipments.Add(id, DataBase.Equipments.Get(id));
                break;
        }
    }

    public void OnUnequip(int id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                _ownWeapons.Remove(id);
                break;
            case ItemType.Consumable:
                _ownConsumable.Remove(id);
                break;
            case ItemType.Equipments:
                _ownEquipments.Remove(id);
                break;
        }
    }

    public void ItemInit()
    {
        // 스테이지 시작 시 호출되어 플레이어 스탯, 아이템 생성 등 처리하기
        WeaponInit();
        SetPlayerStats();
    }

    public void WeaponInit()
    {
        foreach(var weapon in _ownWeapons)
        {
            Instantiate(weapon.Value.Prefab);
            _playerStats.meleeAtk = weapon.Value.PhysicalAtk;
            _playerStats.magicAtk = weapon.Value.MagicAtk;
            _playerStats.attackSpeed = weapon.Value.AtkSpeed;
            _playerStats.critRate = weapon.Value.CritRate;
            _playerStats.critDamage = weapon.Value.CritDamage;
            _playerStats.attackRange = weapon.Value.Range;
            _playerStats.lifeSteal = weapon.Value.LifeSteal;
        }
    }

    public void SetPlayerStats()
    {

    }
}
