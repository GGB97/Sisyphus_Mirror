using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Constants;

public class AchievementUI : UI_Base
{
    UIManager _uiManager;
    QuestManager _questManager;

    [SerializeField]
    private GameObject achievementSlotPrefab;
    [SerializeField]
    private GameObject content1;//일일 퀘스트
    [SerializeField]
    private GameObject content2;//업적
    private int slotCount = 0;
    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _questManager = QuestManager.Instance;
    }
    private void OnEnable()
    {
        _uiManager.AddActiveUI(gameObject);

    }
    private void OnDisable()
    {
        _uiManager.RemoveActiveUI(gameObject);
    }
    public void CreateDailySlot()//일일 퀘스트 슬롯 생성
    {
        List<int> dailyQuestList = _questManager.dailyQuestList;

        foreach (var questId in dailyQuestList) 
        {
            GameObject go = Instantiate(achievementSlotPrefab, content1.transform);
            AchievementSlot aSlot = go.GetComponent<AchievementSlot>();
            aSlot.Init(questId,QuestMode.DailyQuest);
        }
    }
    public void CreateAchievementSlot()
    {
        List<int> AchievementList = _questManager.GetAchievementQuestIdList();//업적의 리스트만 반환
        foreach (var questId in AchievementList)
        {
            GameObject go = Instantiate(achievementSlotPrefab, content2.transform);
            AchievementSlot aSlot = go.GetComponent<AchievementSlot>();
            aSlot.Init(questId, QuestMode.Achievement);
        }
        //퀘스트 진행 중인거와 클리어에서 업적 퀘스트의 id 리스트를 받아옴
    }
}
