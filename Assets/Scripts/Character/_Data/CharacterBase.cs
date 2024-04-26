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

    public float shield;

    public float attackRange;
    public float physicalAtk;
    public float magicAtk;

    public float def;

    public float attackSpeed;
    public float moveSpeed;

    public float knockbackPower;
    public float dashRange; //
    public float dashCoolTime; //

    public float critRate;
    public float critDamage;

    public float lifeSteal;
    public float weight; //

    public int _lv; //
    public float _exp; //
    public float _maxEXP; //

    public int _startItemID; //
    public int _startInventory; //

    public Status()
    {
        Init();
    }

    public virtual void Init()
    {
        health = maxHealth;
    }

    public void InitStatus(Status baseStat, Status modifier, bool isPlayer)
    {
        maxHealth = Mathf.Ceil(baseStat.maxHealth + modifier.maxHealth);
        if(maxHealth <= 1) maxHealth = 1;
        health = baseStat.health + modifier.health;
        if(health > maxHealth) health = maxHealth;

        attackRange = baseStat.attackRange + modifier.attackRange;
        physicalAtk = baseStat.physicalAtk + modifier.physicalAtk;
        magicAtk = baseStat.magicAtk + modifier.magicAtk;

        def = baseStat.def + modifier.def;

        attackSpeed = baseStat.attackSpeed + modifier.attackSpeed;

        float speed = Mathf.Floor(modifier.moveSpeed / 10 * 100) / 100;
        if (isPlayer)
        {
            float playerSpeed = GameManager.Instance.Player.Data.moveSpeed;
            if (speed <= 0)
                moveSpeed = baseStat.moveSpeed + (Mathf.Floor(playerSpeed * speed * 10) / 10);
            else moveSpeed = baseStat.moveSpeed + (Mathf.Ceil(playerSpeed * speed * 10) / 10);
        }
        else moveSpeed = baseStat.moveSpeed + modifier.moveSpeed;

        knockbackPower = baseStat.knockbackPower + modifier.knockbackPower;
        dashRange = baseStat.dashRange + modifier.dashRange;
        dashCoolTime = baseStat.dashCoolTime + modifier.dashCoolTime;

        critRate = baseStat.critRate + modifier.critRate;
        critDamage = baseStat.critDamage + modifier.critDamage;

        lifeSteal = baseStat.lifeSteal + modifier.lifeSteal;
        weight = baseStat.weight + modifier.weight;
    }

    public void SyncHealth()
    {
        health = maxHealth;
    }

    public virtual void Init_EnemyModifier(Status baseStat, EnemyRank rank)
    {
        #region 제곱적 증가 예시
        // base = 100, 스테이지당 증가량 = 2
        // 실 적용 증가량 = 증가량 * (현재 층수^2)

        // (1~10 스테이지 기준)
        // [102, 108, 118, 132, 150, 172, 198, 228, 262, 300] 제곱적 증가
        #endregion

        int stage = DungeonManager.Instance.currnetstage;

        switch (rank)
        {
            case EnemyRank.Boss: // 보스는 처음엔 기본으로 그다음부터 N퍼센트만큼 ex) 0.5 기준(5)500 -> (10)750 -> (15)1000
                int bossStage = stage / 5;
                if (bossStage != 0)
                    bossStage -= 1;
                maxHealth = baseStat.maxHealth * (bossStage * EnemyStageModifier.bossMaxHealth);
                break;
            default:
                maxHealth = EnemyStageModifier.maxHealth * (stage * stage);
                break;
        }

        physicalAtk = EnemyStageModifier.physicalAtk * (stage * stage);
        magicAtk = EnemyStageModifier.magicAtk * (stage * stage);
    }
}