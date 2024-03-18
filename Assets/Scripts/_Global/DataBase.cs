using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : SingletoneBase<DataBase>
{
    [SerializeField] EnemyDB _enemyStat;
    [SerializeField] WeaponDB _weapon;
    [SerializeField] PlayerDB _player;
    [SerializeField] ProjectileDB _Projectile; 

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
            if(Instance._weapon == null)
                Instance._weapon = new WeaponDB();

            return Instance._weapon;
        }
    }

    public static PlayerDB Player
    {
        get
        {
            if( Instance._player == null)
                Instance._player = new PlayerDB();

            return Instance._player;
        }
    }

    public static ProjectileDB Projectile
    {
        get
        {
            if (Instance._player == null)
                Instance._player = new PlayerDB();

            return Instance._Projectile;
        }
    }
}
