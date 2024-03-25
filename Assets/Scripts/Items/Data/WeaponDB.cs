using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDB
{
    private Dictionary<int, WeaponData> _weapons = new Dictionary<int, WeaponData>();
    private List<int> idList = new List<int>(); //변경점

    public WeaponDB()
    {
        var res = Resources.Load<WeaponDB_Sheet>("Items/Data/WeaponDB_Sheet");
        var weaponSO = Object.Instantiate(res);
        var entities = weaponSO.Entities;

        if (entities == null || entities.Count <= 0) return;

        var entityCount = entities.Count;

        for(int i = 0; i < entityCount; i++)
        {
            var weapon = entities[i];

            if (_weapons.ContainsKey(weapon.Id)) _weapons[weapon.Id] = weapon;
            else
            { 
                _weapons.Add(weapon.Id, weapon);
                idList.Add(weapon.Id); //변경점
            } 
        }
    }

    public WeaponData Get(int id)
    {
        if(_weapons.ContainsKey(id)) return _weapons[id];

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

    public List<int> ReturnAllWeaponID()
    {
        return idList;
    }

    public IEnumerator WeaponDBEnumerator()
    {
        return _weapons.GetEnumerator();
    }
}
