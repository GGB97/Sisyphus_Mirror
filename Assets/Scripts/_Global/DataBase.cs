using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : SingletoneBase<DataBase>
{
    [SerializeField] EnemyDB _enemyStat;
    [SerializeField] WeaponDB _Weapon;

    public static EnemyDB EnemyStats
    {
        get
        {
            if (Instance._enemyStat == null)
                Instance._enemyStat = new EnemyDB();

            return Instance._enemyStat;
        }
    }

    public static WeaponDB Weapon
    {
        get
        {
            if(Instance._Weapon == null)
                Instance._Weapon = new WeaponDB();

            return Instance._Weapon;
        }
    }
}
