using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Transform weaponContainer;

    [SerializeField] PlayerBaseData _playerStats;
    [SerializeField] Status _modifier = new Status();

    private List<WeaponData> _ownWeapons = new List<WeaponData>();
    private List<EquipmentsData> _ownEquipments = new List<EquipmentsData>();
    private List<ConsumableData> _ownConsumable = new List<ConsumableData>();

    public List<GameObject> weaponPrefabs = new List<GameObject>();

    public float itemWeight = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InventoryController.Instance.nextStage += RemoveAllItems;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player = PlayerTransform.GetComponent<Player>();
        _playerStats = Player.Data;
        init();
        ItemInit();
    }

    private void init()
    {
        InventoryController.Instance.AddStartWeapon(DataBase.Weapon.Get(_playerStats.startItemID));
        InventoryController.Instance.AddRuneStone(DataBase.Equipments.Get(_playerStats.startItemID));
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
        ResetModifierStat();
        // 스테이지 시작 시 호출되어 플레이어 스탯, 아이템 생성 등 처리하기
        WeaponInit();
        EquipmentsInit();

        _playerStats.InitStatus(_playerStats, _modifier);
    }

    public void WeaponInit()
    {
        foreach(var weapon in _ownWeapons)
        {
            GameObject go = Instantiate(weapon.Prefab, weaponContainer);
            weaponPrefabs.Add(go);
          
            _modifier.physicalAtk += weapon.PhysicalAtk;
            _modifier.magicAtk += weapon.MagicAtk;

            _modifier.attackSpeed += weapon.AtkSpeed;

            //_modifier.knockbackPower = 0;
            //_modifier.dashRange = 0;
            //_modifier.dashCoolTime = 0;

            _modifier.critRate += weapon.CritRate;
            _modifier.critDamage += weapon.CritDamage;

            //_modifier.attackRange += weapon.Range;

            _modifier.lifeSteal += weapon.LifeSteal;

            itemWeight += weapon.Weight;
        }
    }

    public void EquipmentsInit()
    {
        // TODO : 플레이어의 스탯 Status를 수정하기 -> 기범님께 방법을 여쭈어볼 것
        foreach (var equipment in _ownEquipments)
        {
            _modifier.maxHealth += equipment.Health;

            _modifier.physicalAtk += equipment.PhysicalAtk;
            _modifier.magicAtk += equipment.MagicAtk;

            _modifier.def += equipment.Def;

            _modifier.attackSpeed += equipment.AtkSpeed;
            _modifier.moveSpeed += equipment.MoveSpeed;

            _modifier.critRate += equipment.CritRate;
            _modifier.critDamage += equipment.CritDamage;

            _modifier.lifeSteal += equipment.LifeSteal;

            itemWeight += equipment.Weight;
        }

        _modifier.maxHealth -= itemWeight / 10;
    }

    public void RemoveAllItems()
    {
        foreach(var weapon in weaponPrefabs)
        {
            Destroy(weapon);
        }
        //foreach (var weapon in _ownWeapons)
        //{
        //    _playerStats.meleeAtk -= weapon.PhysicalAtk;
        //    _playerStats.magicAtk -= weapon.MagicAtk;
        //    //_playerStats.attackSpeed += weapon.AtkSpeed;
        //    _playerStats.critRate -= weapon.CritRate;
        //    _playerStats.critDamage -= weapon.CritDamage;
        //    //_playerStats.attackRange += weapon.Range;
        //    _playerStats.lifeSteal -= weapon.LifeSteal;
        //}

        ResetPlayerStat();
    }

    public void ResetModifierStat()
    {
        _modifier.maxHealth = 0;
        _modifier.attackRange = 0;
        _modifier.physicalAtk = 0;
        _modifier.magicAtk = 0;

        _modifier.def = 0;

        _modifier.attackSpeed = 0;
        _modifier.moveSpeed = 0;

        _modifier.knockbackPower = 0;
        _modifier.dashRange = 0;
        _modifier.dashCoolTime = 0;

        _modifier.critRate = 0;
        _modifier.critDamage = 0;

        _modifier.lifeSteal = 0;

        itemWeight = 0;
    }

    private void ResetPlayerStat()
    {
        _modifier.maxHealth = -_modifier.maxHealth;
        _modifier.attackRange = -_modifier.attackRange;
        _modifier.physicalAtk = -_modifier.physicalAtk;
        _modifier.magicAtk = -_modifier.magicAtk;

        _modifier.def = -_modifier.def;

        _modifier.attackSpeed =- _modifier.attackSpeed;
        _modifier.moveSpeed = -_modifier.moveSpeed;

        _modifier.knockbackPower = -_modifier.knockbackPower;
        _modifier.dashRange = -_modifier.dashRange;
        _modifier.dashCoolTime = -_modifier.dashCoolTime;

        _modifier.critRate = -_modifier.critRate;
        _modifier.critDamage = -_modifier.critDamage;

        _modifier.lifeSteal = -_modifier.lifeSteal;

        _playerStats.InitStatus(_playerStats, _modifier);
    }
}
