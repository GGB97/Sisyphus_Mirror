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
    public Transform weaponPivot;

    [SerializeField] PlayerBaseData _playerStats;
    public Dictionary<ItemType, List<InventoryItem>> ownItems;
    //private Dictionary<ItemType, WeaponData> _ownWeapons = new Dictionary<ItemType, WeaponData>();
    //private Dictionary<ItemType, EquipmentsData> _ownEquipments = new Dictionary<ItemType, EquipmentsData>();
    //private Dictionary<ItemType, ConsumableData> _ownConsumable = new Dictionary<ItemType, ConsumableData>();
    private List<WeaponData> _ownWeapons = new List<WeaponData>();
    private List<EquipmentsData> _ownEquipments = new List<EquipmentsData>();
    private List<ConsumableData> _ownConsumable = new List<ConsumableData>();

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
                _ownWeapons.Add(DataBase.Weapon.Get(id));
                break;
            case ItemType.Consumable:
                _ownConsumable.Add(DataBase.Consumable.Get(id));
                break;
            case ItemType.Equipments:
                _ownEquipments.Add(DataBase.Equipments.Get(id));
                break;
        }
    }

    public void OnUnequip(int id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                //_ownWeapons.Find(x => x.Id == id);
                _ownWeapons.Remove(_ownWeapons.Find(x => x.Id == id));
                break;
            case ItemType.Consumable:
                _ownConsumable.Remove(_ownConsumable.Find(x => x.Id == id));
                break;
            case ItemType.Equipments:
                _ownEquipments.Remove(_ownEquipments.Find(x => x.Id == id));
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
            Instantiate(weapon.Prefab, weaponPivot);
            _playerStats.meleeAtk += weapon.PhysicalAtk;
            _playerStats.magicAtk += weapon.MagicAtk;
            //_playerStats.attackSpeed += weapon.AtkSpeed;
            _playerStats.critRate += weapon.CritRate;
            _playerStats.critDamage += weapon.CritDamage;
            //_playerStats.attackRange += weapon.Range;
            _playerStats.lifeSteal += weapon.LifeSteal;
        }
    }

    public void SetPlayerStats()
    {
        foreach(var equipment in _ownEquipments)
        {

        }
    }
}
