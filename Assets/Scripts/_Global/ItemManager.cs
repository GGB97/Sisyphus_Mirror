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

    DungeonManager _dungeonManager;

    public Transform PlayerTransform { get; private set; }
    public Player Player { get; private set; }
    public Transform weaponContainer;

    [SerializeField] Status _playerStats;
    [SerializeField] Status _modifier = new Status();

    private List<WeaponData> _ownWeapons = new List<WeaponData>();
    private List<EquipmentsData> _ownEquipments = new List<EquipmentsData>();
    private List<ConsumableData> _ownConsumable = new List<ConsumableData>();

    private WeaponData _tempWeapon;
    private EquipmentsData _tempEquipments;
    private ConsumableData _tempConsumable;
    private List<ConsumableData> _usedConsumable = new List<ConsumableData>();
    private List<int> _consumableStageDuration = new List<int>();

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

        _dungeonManager = DungeonManager.Instance;
        _dungeonManager.OnStageClear += SetConsumableDuration;

        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player = PlayerTransform.GetComponent<Player>();
        //Player = GameManager.Instance.Player;
        _playerStats = Player.currentStat;
        //init();
    }

    public void init()
    {
        InventoryController.Instance.AddStartWeapon(DataBase.Weapon.Get(Player.Data._startItemID));
        //InventoryController.Instance.AddRuneStone(DataBase.Equipments.Get(_playerStats.startItemID));
        InventoryController.Instance.AddRuneStone(DataBase.Equipments.Get(10112011));

        ItemInit();
    }

    public void OnEquip(int id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                _tempWeapon = DataBase.Weapon.Get(id);
                _ownWeapons.Add(_tempWeapon);
                ModifyPlayerStat(_tempWeapon, true);
                break;
            case ItemType.Consumable:
                _tempConsumable = DataBase.Consumable.Get(id);
                _ownConsumable.Add(_tempConsumable);
                ConsumableToInventory(_tempConsumable, true);
                break;
            case ItemType.Equipments:
                _tempEquipments = DataBase.Equipments.Get(id);
                _ownEquipments.Add(_tempEquipments);
                ModifyPlayerStat(_tempEquipments, true);
                break;
        }
    }

    public void OnUnequip(int id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                _tempWeapon = _ownWeapons.Find(x => x.Id == id);
                _ownWeapons.Remove(_tempWeapon);
                ModifyPlayerStat(_tempWeapon, false);
                break;
            case ItemType.Consumable:
                _tempConsumable = _ownConsumable.Find(x => x.Id == id);
                _ownConsumable.Remove(_tempConsumable);
                ConsumableToInventory(_tempConsumable, false);
                break;
            case ItemType.Equipments:
                _tempEquipments = _ownEquipments.Find(x => x.Id == id);
                _ownEquipments.Remove(_tempEquipments);
                ModifyPlayerStat(_tempEquipments, false);
                break;
        }
    }

    public void ItemInit()
    {
        ResetModifierStat();
        // 스테이지 시작 시 호출되어 플레이어 스탯, 아이템 생성 등 처리하기
        WeaponInit();
    }

    public void ModifyPlayerStat(WeaponData weapon, bool isEquip)
    {
        ResetModifierStat();

        if (isEquip)
        {
            //_modifier.physicalAtk += weapon.PhysicalAtk;
            //_modifier.magicAtk += weapon.MagicAtk;

            //_modifier.attackSpeed += weapon.AtkSpeed;

            _modifier.critRate += weapon.CritRate;
            _modifier.critDamage += weapon.CritDamage;

            _modifier.lifeSteal += weapon.LifeSteal;
            _modifier.weight += weapon.Weight;
            _modifier.maxHealth -= Mathf.Round(weapon.Weight * 10 / 20) / 10;
            _modifier.moveSpeed -= Mathf.Round(weapon.Weight * 10 / 20) / 10;

            itemWeight += weapon.Weight;
        }
        else
        {
            //_modifier.physicalAtk -= weapon.PhysicalAtk;
            //_modifier.magicAtk -= weapon.MagicAtk;

            //_modifier.attackSpeed -= weapon.AtkSpeed;

            _modifier.critRate -= weapon.CritRate;
            _modifier.critDamage -= weapon.CritDamage;

            _modifier.lifeSteal -= weapon.LifeSteal;
            _modifier.weight -= weapon.Weight;
            _modifier.maxHealth += Mathf.Round(weapon.Weight * 10 / 20) / 10;
            _modifier.moveSpeed += Mathf.Round(weapon.Weight * 10 / 20) / 10;

            itemWeight -= weapon.Weight;
        }

        _playerStats.InitStatus(Player.currentStat, _modifier, true);
    }

    public void ModifyPlayerStat(EquipmentsData equipment, bool isEquip)
    {
        ResetModifierStat();

        if (isEquip)
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
            _modifier.weight += equipment.Weight;
            _modifier.maxHealth -= Mathf.Round(equipment.Weight * 10 / 20) / 10;
            _modifier.moveSpeed -= Mathf.Round(equipment.Weight * 10 / 20) / 10;

            itemWeight += equipment.Weight;
        }
        else
        {
            _modifier.maxHealth -= equipment.Health;

            _modifier.physicalAtk -= equipment.PhysicalAtk;
            _modifier.magicAtk -= equipment.MagicAtk;

            _modifier.def -= equipment.Def;

            _modifier.attackSpeed -= equipment.AtkSpeed;
            _modifier.moveSpeed -= equipment.MoveSpeed;

            _modifier.critRate -= equipment.CritRate;
            _modifier.critDamage -= equipment.CritDamage;

            _modifier.lifeSteal -= equipment.LifeSteal;
            _modifier.weight -= equipment.Weight;
            _modifier.maxHealth += Mathf.Round(equipment.Weight * 10 / 20) / 10;
            _modifier.moveSpeed += Mathf.Round(equipment.Weight * 10 / 20) / 10;

            itemWeight -= equipment.Weight;
        }

        _playerStats.InitStatus(Player.currentStat, _modifier, true);
    }

    public void ModifyPlayerStat(ConsumableData consumable, bool isUsed)
    {
        ResetModifierStat();

        if (isUsed)
        {
            _modifier.health += consumable.Health;
            _modifier.physicalAtk += consumable.PhysicalAtk;
            _modifier.magicAtk += consumable.MagicAtk;

            _modifier.def += consumable.Def;

            _modifier.attackSpeed += consumable.AttackSpeed;
            _modifier.moveSpeed += consumable.MoveSpeed;
        }
        else
        {
            _modifier.health -= consumable.Health;
            _modifier.physicalAtk -= consumable.PhysicalAtk;
            _modifier.magicAtk -= consumable.MagicAtk;

            _modifier.def -= consumable.Def;

            _modifier.attackSpeed -= consumable.AttackSpeed;
            _modifier.moveSpeed -= consumable.MoveSpeed;
        }

        _playerStats.InitStatus(Player.currentStat, _modifier, true);
    }

    public void ConsumableToInventory(ConsumableData consumable, bool isAdded)
    {
        ResetModifierStat();

        if (isAdded)
        {
            _modifier.weight += consumable.Weight;

            itemWeight += consumable.Weight;
            _modifier.maxHealth -= Mathf.Round(consumable.Weight / 20);
            //_modifier.moveSpeed -= Mathf.Round(consumable.Weight / 20);
            _modifier.moveSpeed -= Mathf.Round(consumable.Weight * 10 / 20) / 10;
        }
        else
        {
            _modifier.weight -= consumable.Weight;

            itemWeight -= consumable.Weight;
            _modifier.maxHealth += Mathf.Round(consumable.Weight / 20);
            //_modifier.moveSpeed += Mathf.Round(consumable.Weight / 20);
            _modifier.moveSpeed += Mathf.Round(consumable.Weight * 10 / 20) / 10;
        }

        _playerStats.InitStatus(Player.currentStat, _modifier, true);
    }

    public void WeaponInit()
    {
        foreach (var weapon in _ownWeapons)
        {
            GameObject go = Instantiate(weapon.Prefab, weaponContainer);
            if (go.TryGetComponent<MeleeWeapon>(out MeleeWeapon _weapon))
            {
                _weapon.Init(weapon.Id);
            }
            else go.GetComponent<RangeWeapon>().Init(weapon.Id);
            weaponPrefabs.Add(go);
        }
    }

    public void RemoveAllItems()
    {
        foreach (var weapon in weaponPrefabs)
        {
            Destroy(weapon);
        }

        ItemInit();
    }

    public void ResetModifierStat()
    {
        _modifier.maxHealth = 0;

        _modifier.health = 0;
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
        _modifier.weight = 0;

        itemWeight = 0;
    }

    // 사용 아이템 사용 시 호출해주세요!
    public void UseConsumable(ConsumableData consumable)
    {
        if (consumable.StageDuration != 0)
        {
            _usedConsumable.Add(consumable);
            _consumableStageDuration.Add(consumable.StageDuration);
        }

        ModifyPlayerStat(consumable, true);
    }

    // 아이템의 지속 시간을 갱신 및 남은 지속 시간 체크
    private void SetConsumableDuration()
    {
        if (_usedConsumable.Count != 0)
        {
            for (int i = _usedConsumable.Count - 1; i >= 0; --i)
            {
                _consumableStageDuration[i]--;
                if (_consumableStageDuration[i] <= 0)
                {
                    ModifyPlayerStat(_usedConsumable[i], false);
                    _usedConsumable.RemoveAt(i);
                }
            }
        }
    }

    private void OnDisable()
    {
        InventoryController.Instance.nextStage -= RemoveAllItems;
        _dungeonManager.OnStageClear -= SetConsumableDuration;
    }
}
