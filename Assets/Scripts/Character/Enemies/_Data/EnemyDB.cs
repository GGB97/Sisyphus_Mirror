using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyDB
{
    Dictionary<int, EnemyBaseStat> _stats = new();

    public EnemyDB()
    {
        var res = Resources.Load<EnemyDB_Sheet>(EnemyData.DBPath);
        var enemySO = Object.Instantiate(res);
        var entities = enemySO.Entities;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var stat = entities[i];

            if (_stats.ContainsKey(stat.id))
                _stats[stat.id] = stat;
            else
                _stats.Add(stat.id, stat);
        }
    }

    public EnemyBaseStat Get(int id)
    {
        if (_stats.ContainsKey(id))
            return _stats[id];

        return null;
    }
}
