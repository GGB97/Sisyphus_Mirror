using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterBase : Status
{
    [Header("Info")]
    public int id;

    public string prefabPath;

    public CharacterBase()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }
}

[System.Serializable]
public class Status
{
    [Header("Stat")]
    public float health;
    public float maxHealth;

    public float attackRange;
    public float meleeAtk;
    public float magicAtk;

    public float def;

    public float attackSpeed;
    public float moveSpeed;

    public Status()
    {
        Init();
    }

    public virtual void Init()
    {
        health = maxHealth;
    }

    public void InitStatus(Status baseStat, Status modifier)
    {
        maxHealth = baseStat.maxHealth + modifier.maxHealth;
        health = maxHealth;

        attackRange = baseStat.attackRange + modifier.attackRange;
        meleeAtk = baseStat.meleeAtk + modifier.meleeAtk;
        magicAtk = baseStat.magicAtk + modifier.magicAtk;

        def = baseStat.def + modifier.def;

        attackSpeed = baseStat.attackSpeed + modifier.attackSpeed;
        moveSpeed = baseStat.moveSpeed + modifier.moveSpeed;
    }
}