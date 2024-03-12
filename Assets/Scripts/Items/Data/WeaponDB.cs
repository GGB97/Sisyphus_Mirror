using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDB
{
    private Dictionary<int, WeaponData> _weapons = new Dictionary<int, WeaponData>();

    public WeaponDB()
    {
        var res = Resources.Load<WeaponDB_Sheet>("/Items/Data/WeaponDB_Sheet");
        var weaponSO = Object.Instantiate(res);
        var entities = weaponSO.Entities;

        if (entities == null || entities.Count <= 0) return;

        var entityCount = entities.Count;

        for(int i = 0; i < entityCount; i++)
        {
            var weapon = entities[i];

            if(_weapons.ContainsKey(weapon.Id)) _weapons[weapon.Id] = weapon;
            else _weapons.Add(weapon.Id, weapon);
        }
    }

    public WeaponData Get(int id)
    {
        if(_weapons.ContainsKey(id)) return _weapons[id];

        return null;
    }

    public IEnumerator WeaponDBEnumerator()
    {
        return _weapons.GetEnumerator();
    }
}
