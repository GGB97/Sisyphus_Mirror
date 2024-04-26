using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCanvas : MonoBehaviour
{
    [SerializeField]
    private AchievementUI achievementUI;
    private QuestManager questManager;
    private QuestSaveManager questSaveManager;
    private void Awake()
    {
        questManager = QuestManager.Instance;
        questSaveManager = QuestSaveManager.Instance;
    }
    private void OnEnable()
    {
        questSaveManager.loadDataEvent += CreateSlot;
    }
    private void OnDisable()
    {
        questSaveManager.loadDataEvent -= CreateSlot;
    }
    public void CreateSlot()
    {
        DestroyAll();
        achievementUI.CreateDailySlot();//일일 퀘스트 슬롯 생성
        achievementUI.CreateAchievementSlot();//업적 슬롯 생성
    }
    public void DestroyAll()
    {
        if(achievementUI.gameObject.activeSelf == true)//켜져있다면 끈다
            achievementUI.gameObject.SetActive(false);

        achievementUI.DestroyAllQuestSlot();
    }
}
