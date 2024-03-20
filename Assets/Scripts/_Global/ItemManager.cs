using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void OnEquip();
    void OnUnequip();
}

public class ItemManager : SingletoneBase<ItemManager>
{
    public Player Player { get; private set; }
    [SerializeField] PlayerBaseData _playerStats;
    public Dictionary<ItemType, List<InventoryItem>> ownItems;
    private List<WeaponData> _weapons;
    private List<EquipmentsData> _equipments;
    private List<ConsumableData> _consumable;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        if (Player != null)
        {
            // TODO : Player 스탯 반영하기
            
        }
    }

    public void OnUnequip(int id, ItemType itemType)
    {

    }
}
