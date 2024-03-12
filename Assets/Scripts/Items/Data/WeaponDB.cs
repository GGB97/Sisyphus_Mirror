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
        
    }
}
