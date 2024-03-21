using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    CharacterBehaviour character;
    Status stat;

    private void Awake()
    {
        character = GetComponent<CharacterBehaviour>();
        stat = character.currentStat;
    }

    public void ChangeHealth(float value)
    {
        stat.health = value; // 방어력 포함해서 계산해야할듯?

        if(stat.health <= 0)
        {
            stat.health = 0;
            character.isDie = true;
        }
        else if (stat.health > stat.maxHealth)
        {
            stat.health -= stat.maxHealth;
        }
    }

    public void TakeDamage(float value)
    {
        stat.health -= value;

        Debug.Log($"Take Damage : {value}, name : {gameObject.name}, health : {stat.health}");

        if(stat.health <= 0)
        {
            stat.health = 0;
            character.isDie = true;
        }
        else
        {
            character.isHit = true;
        }



    }
}
