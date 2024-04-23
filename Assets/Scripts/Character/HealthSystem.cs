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

    [SerializeField] ParticleSystem Ps_Healing;

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
        if (character.isInvincibility)
        {
            // 데미지를 받지 않는 상태
            return;
        }

        stat = character.currentStat;
        int damage = Mathf.RoundToInt(value - (value * (stat.def * 0.01f)));

        #region 보호막 데미지 처리
        stat.shield -= damage;
        if (stat.shield < 0)
        {
            stat.health -= Mathf.Abs(stat.shield);
            stat.shield = 0;
        }
        #endregion

        #region 체력 감소 처리
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
        #endregion

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


        if (Ps_Healing != null)
        {
            Ps_Healing.Play();
        }

        ShowDamage(value, type);
    }

    void ShowDamage(float value, DamageType type)
    {
        if (textQueue.Count == 0)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/DamageText"), damageCanvas.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;

            textQueue.Enqueue(go.GetComponent<TMP_Text>());
        }

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

        float rand = Random.Range(-0.35f, 0.35f);
        text.transform.localPosition = new(rand, 0, 0);

        text.DOFade(0, 0.5f);
        text.transform.DOScale(1.2f, 0.1f).OnComplete(() =>
        {
            text.transform.DOScale(1f, 0.1f);
        });
        text.transform.DOLocalMoveY(0.5f, 0.75f).OnComplete(() =>
        {
            OnShowDamageComplete(text);
        });
    }
    void OnShowDamageComplete(TMP_Text text)
    {
        text.gameObject.SetActive(false);
        text.color = Color.white;
        text.transform.localPosition = Vector3.zero;
        text.transform.localRotation = Quaternion.identity;
        text.transform.localScale = Vector3.one;

        textQueue.Enqueue(text);
    }
}
