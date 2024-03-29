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

    private List<WeaponData> _ownWeapons = new List<WeaponData>();
    private List<EquipmentsData> _ownEquipments = new List<EquipmentsData>();
    private List<ConsumableData> _ownConsumable = new List<ConsumableData>();

    public List<GameObject> weaponPrefabs = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InventoryController.Instance.nextStage += RemoveAllWeapons;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player = PlayerTransform.GetComponent<Player>();
        _playerStats = Player.Data;
        init();
        ItemInit();
    }

    private void init()
    {
        InventoryController.Instance.AddStartWeapon(DataBase.Weapon.Get(_playerStats.startItemID));
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
                Debug.Log("Unequip");
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
            GameObject go = Instantiate(weapon.Prefab, weaponPivot);
            weaponPrefabs.Add(go);
            _playerStats.meleeAtk += weapon.PhysicalAtk;
            _playerStats.magicAtk += weapon.MagicAtk;
            //_playerStats.attackSpeed += weapon.AtkSpeed;
            _playerStats.critRate += weapon.CritRate;
            _playerStats.critDamage += weapon.CritDamage;
            //_playerStats.attackRange += weapon.Range;
            _playerStats.lifeSteal += weapon.LifeSteal;
        }
    }

    public void EquipmentsInit()
    {
        // TODO : 플레이어의 스탯 Status를 수정하기 -> 기범님께 방법을 여쭈어볼 것
        foreach (var equipment in _ownEquipments)
        {
            
        }
    }

    public void RemoveAllWeapons()
    {
        foreach(var weapon in weaponPrefabs)
        {
            Destroy(weapon);
        }
        foreach (var weapon in _ownWeapons)
        {
            _playerStats.meleeAtk -= weapon.PhysicalAtk;
            _playerStats.magicAtk -= weapon.MagicAtk;
            //_playerStats.attackSpeed += weapon.AtkSpeed;
            _playerStats.critRate -= weapon.CritRate;
            _playerStats.critDamage -= weapon.CritDamage;
            //_playerStats.attackRange += weapon.Range;
            _playerStats.lifeSteal -= weapon.LifeSteal;
        }
    }

    public void SetPlayerStats()
    {
        foreach(var equipment in _ownEquipments)
        {

        }
    }
}
