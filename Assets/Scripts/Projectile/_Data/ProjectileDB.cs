using System.Collections.Generic;
using UnityEngine;

public class ProjectileDB
{
    Dictionary<int, ProjectileData> _datas = new();

    public ProjectileDB()
    {
        var res = Resources.Load<ProjectileDB_Sheet>(DBPath.ProjectileDB);
        var enemySO = Object.Instantiate(res);
        var entities = enemySO.Entities;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var stat = entities[i];

            if (_datas.ContainsKey(stat.id))
                _datas[stat.id] = stat;
            else
                _datas.Add(stat.id, stat);
        }
    }

    public ProjectileData Get(int id)
    {
        if (_datas.ContainsKey(id))
            return _datas[id];

        return null;
    }
}
