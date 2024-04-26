using System.Collections.Generic;
using UnityEngine;

public class PlayerDB
{
    private Dictionary<int, PlayerBaseData> _player = new();

    public PlayerDB()
    {
        var res = Resources.Load<PlayerDB_Sheet>("Player/Data/PlayerDB_Sheet");
        var playerSO = Object.Instantiate(res);
        var entities = playerSO.Entities;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var player = entities[i];

            if (_player.ContainsKey(player.id))
                _player[player.id] = player;
            else
                _player.Add(player.id, player);
        }
    }

    public PlayerBaseData Get(int id)
    {
        if (_player.ContainsKey(id))
            return _player[id];

        return null;
    }

}
