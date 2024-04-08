using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : SingletoneBase<DataBase>
{
    [SerializeField] EnemyDB _enemyStat;
    [SerializeField] WeaponDB _weapon;
    [SerializeField] EquipmentsDB _equipments;
    [SerializeField] ConsumableDB _consumable;
    [SerializeField] PlayerDB _player;
    [SerializeField] ProjectileDB _Projectile;
    [SerializeField] PlayerUpgradeDB _PlayerUpgrade;
    [SerializeField] QuestDB _quest;

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

    public static ConsumableDB Consumable
    {
        get
        {
            if (Instance._consumable == null)
                Instance._consumable = new ConsumableDB();

            return Instance._consumable;
        }
    }

    public static EquipmentsDB Equipments
    {
        get
        {
            if (Instance._equipments == null)
                Instance._equipments = new EquipmentsDB();

            return Instance._equipments;
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
            if (Instance._Projectile == null)
                Instance._Projectile = new ProjectileDB();

            return Instance._Projectile;
        }
    }

    public static PlayerUpgradeDB PlayerUpgrade
    {
        get
        {
            if (Instance._PlayerUpgrade == null)
                Instance._PlayerUpgrade = new PlayerUpgradeDB();

            return Instance._PlayerUpgrade;
        }
    }
    public static QuestDB Quest
    {
        get
        {
            if (Instance._quest == null)
                Instance._quest = new QuestDB();

            return Instance._quest;
        }
    }
}
