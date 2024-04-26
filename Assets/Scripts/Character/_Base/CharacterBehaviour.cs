using System;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] protected int id; // DB에서 가져올 ID
    [SerializeField] protected int activeID; // 실행 도중 구분을 위한 ID

    public Status currentStat; // Base + Modifier 가 적용된 진짜 스탯들

    public bool isDie;
    public bool isDieTrigger;
    public Action OnDieEvent;

    public bool isHit;
    public Action OnHitEvent;
    public float knockbackDelay;

    public bool isInvincibility;

    protected virtual void Awake()
    {
        // Active ID 할당
        activeID = GameManager.Instance.SetActiveID();
    }

    public int GetActiveID()
    {
        return activeID;
    }
}