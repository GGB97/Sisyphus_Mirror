using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    CharacterBehaviour character;
    Status stat;

    public Canvas damageCanvas;
    Queue<TMP_Text> textQueue;

    private void Awake()
    {
        character = GetComponent<CharacterBehaviour>();
        stat = character.currentStat;

        if (damageCanvas != null)
        {
            TMP_Text[] textList = damageCanvas.GetComponentsInChildren<TMP_Text>();
            textQueue = new(textList);
            foreach (var text in textList)
            {
                text.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeHealth(float value)
    {
        stat.health = value; // 방어력 포함해서 계산해야할듯?

        if (stat.health <= 0)
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
        //if (gameObject.tag.Equals("Player"))
        //    Debug.Log($"Take Damage : {value}, name : {gameObject.name}, health : {stat.health}");

        if (stat.health <= 0)
        {
            stat.health = 0;
            character.isDie = true;
            character.isDieTrigger = true;
        }
        else
        {
            character.isHit = true;
        }

        ShowDamage(value);
    }

    void ShowDamage(float value) // Player는 지금 DamageCanvas가 없음
    {
        TMP_Text text = textQueue.Dequeue();
        
        text.text = value.ToString();
        // 데미지 타입별로 색상 조절해도 괜찮을듯

        text.gameObject.SetActive(true);

        text.DOFade(0, 1);
        text.transform.DOLocalMoveY(0.5f, 1).OnComplete(()=>
        {
            OnShowDamageComplete(text);
        });
    }
    void OnShowDamageComplete(TMP_Text text)
    {
        text.gameObject.SetActive(false);
        text.color = Color.white;
        text.transform.localPosition = Vector3.zero;

        textQueue.Enqueue(text);
    }
}
