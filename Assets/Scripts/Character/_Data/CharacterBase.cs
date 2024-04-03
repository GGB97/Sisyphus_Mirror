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
    public float physicalAtk;
    public float magicAtk;

    public float def;

    public float attackSpeed;
    public float moveSpeed;

    public float knockbackPower;
    public float dashRange;
    public float dashCoolTime;

    public float critRate;
    public float critDamage;

    public float lifeSteal;

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
        physicalAtk = baseStat.physicalAtk + modifier.physicalAtk;
        magicAtk = baseStat.magicAtk + modifier.magicAtk;

        def = baseStat.def + modifier.def;

        attackSpeed = baseStat.attackSpeed + modifier.attackSpeed;
        moveSpeed = baseStat.moveSpeed + modifier.moveSpeed;

        knockbackPower = baseStat.knockbackPower + modifier.knockbackPower;
        dashRange = baseStat.dashRange + modifier.dashRange;
        dashCoolTime = baseStat.dashCoolTime + modifier.dashCoolTime;

        critRate = baseStat.critRate + modifier.critRate;
        critDamage = baseStat.critDamage + modifier.critDamage;

        lifeSteal = baseStat.lifeSteal + modifier.lifeSteal;
    }
}