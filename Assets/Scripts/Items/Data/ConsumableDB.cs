using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableDB : MonoBehaviour
{
    private Dictionary<int, ConsumableData> _consumable = new Dictionary<int, ConsumableData>();
    private List<int> idList = new List<int>(); //변경점

    public ConsumableDB()
    {
        var res = Resources.Load<ConsumableDB_Sheet>("Items/Data/ConsumableDB_Sheet");
        var consumableSO = Object.Instantiate(res);
        var entities = consumableSO.Entities;

        if (entities == null || entities.Count <= 0) return;

        var entityCount = entities.Count;

        for (int i = 0; i < entityCount; i++)
        {
            var consumable = entities[i];

            if (_consumable.ContainsKey(consumable.Id)) _consumable[consumable.Id] = consumable;
            else
            {
                _consumable.Add(consumable.Id, consumable);
                idList.Add(consumable.Id); //변경점
            }
        }
    }

    public ConsumableData Get(int id)
    {
        if (_consumable.ContainsKey(id)) return _consumable[id];

        return null;
    }
    public int GetItemId(int index) //변경점
    {
        if (index < 0 && index < idList.Count) //index가 유효하지 않다면
            return 9999;

        return idList[index];
    }
    public int GetItemIdCount()//변경점
    {
        return idList.Count;
    }

    public List<int> ReturnAllConsumableID()
    {
        return idList;
    }

    public IEnumerator ConsumableEnumerator()
    {
        return _consumable.GetEnumerator();
    }
}
