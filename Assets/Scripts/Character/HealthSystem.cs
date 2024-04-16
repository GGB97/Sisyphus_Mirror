using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void TakeDamage(float value, DamageType type)
    {
        int damage = Mathf.RoundToInt(value);
        stat = character.currentStat;

        stat.shield -= damage;
        if (stat.shield < 0)
        {
            stat.health -= Mathf.Abs(stat.shield);
            stat.shield = 0;
        }

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

        if (damage == 0)
            return;

        ShowDamage(damage, type);
    }

    public void TakeHeal(float value, DamageType type)
    {
        stat.health += value;

        if (stat.health > stat.maxHealth)
        {
            stat.health = stat.maxHealth;
        }

        if (value == 0)
            return;

        ShowDamage(value, type);
    }

    void ShowDamage(float value, DamageType type)
    {
        TMP_Text text = textQueue.Dequeue();
        if (text != null)
            text.text = value.ToString();


        // 데미지 타입별로 색상 조절
        switch (type)
        {
            case DamageType.Physical:
                text.color = Color.yellow;
                break;
            case DamageType.Magic:
                text.color = new Color(175f / 255f, 50f / 255f, 207f / 255f);
                break;
            case DamageType.Heal:
                text.color = Color.green;
                break;
        }

        text.gameObject.SetActive(true);

        text.DOFade(0, 1);
        text.transform.DOLocalMoveY(0.5f, 1).OnComplete(() =>
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
