using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsDB
{
    private Dictionary<int, EquipmentsData> _equipments = new Dictionary<int, EquipmentsData>();

    public EquipmentsDB()
    {
        var res = Resources.Load<EquipmentsDB_Sheet>("Items/Data/EquipmentsDB_Sheet");
        var EquipmentSO = Object.Instantiate(res);
        var entities = EquipmentSO.Entities;

        if (entities == null || entities.Count <= 0) return;

        var entityCount = entities.Count;

        for (int i = 0; i < entityCount; i++)
        {
            var equipments = entities[i];

            if (_equipments.ContainsKey(equipments.Id)) _equipments[equipments.Id] = equipments;
            else _equipments.Add(equipments.Id, equipments);
        }
    }

    public EquipmentsData Get(int id)
    {
        if (_equipments.ContainsKey(id)) return _equipments[id];

        return null;
    }

    public IEnumerator EquipmentsDBEnumerator()
    {
        return _equipments.GetEnumerator();
    }
}
