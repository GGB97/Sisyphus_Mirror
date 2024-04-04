using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerUpgradeDB
{
    Dictionary<int, UpgradeData> _data = new();

    public PlayerUpgradeDB()
    {
        var res = Resources.Load<PlayerUpgradeDB_Sheet>(DBPath.PlayerUpgradeDB);
        var SO = Object.Instantiate(res);
        var entities = SO.Entities;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var data = entities[i];

            if (_data.ContainsKey(data.ID))
                _data[data.ID] = data;
            else
                _data.Add(data.ID, data);
        }
    }

    public UpgradeData Get(int id)
    {
        if (_data.ContainsKey(id))
            return _data[id];

        return null;
    }
}
