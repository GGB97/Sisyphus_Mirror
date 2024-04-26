using System.Collections.Generic;
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
        List<AchievementSlot> rewardList = new List<AchievementSlot>();

        foreach (var questId in dailyQuestList)
        {
            GameObject go = Instantiate(achievementSlotPrefab, content1.transform);
            AchievementSlot aSlot = go.GetComponent<AchievementSlot>();
            aSlot.Init(questId,QuestMode.DailyQuest, ref rewardList);
        }
        RewardLayerSetting(ref rewardList);
        rewardList.Clear();
    }
    public void CreateAchievementSlot()
    {
        List<int> AchievementList = _questManager.GetAchievementQuestIdList();//업적의 리스트만 반환
        List<AchievementSlot> rewardList = new List<AchievementSlot>();

        foreach (var questId in AchievementList)
        {
            GameObject go = Instantiate(achievementSlotPrefab, content2.transform);
            AchievementSlot aSlot = go.GetComponent<AchievementSlot>();
            aSlot.Init(questId, QuestMode.Achievement, ref rewardList);
        }
        RewardLayerSetting(ref rewardList);
        rewardList.Clear();
        //퀘스트 진행 중인거와 클리어에서 업적 퀘스트의 id 리스트를 받아옴
    }
    public void DestroyAllQuestSlot()
    {
        Transform[] children1 = content1.GetComponentsInChildren<Transform>();
        Transform[] children2 = content2.GetComponentsInChildren<Transform>();
        // 가져온 자식들을 순회하면서 처리합니다.
        foreach (Transform child in children1)
        {
            if (child == content1.transform)
                continue;
            Destroy(child.gameObject);
        }
        foreach (Transform child in children2)
        {
            if (child == content2.transform)
                continue;
            Destroy(child.gameObject);
        }
    }
    public void RewardLayerSetting(ref List<AchievementSlot> rewardList)
    {
        if (rewardList == null)
            return;

        foreach (var slot in rewardList)
        {
            if (slot != null)
            {
                RectTransform rectTransform = slot.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }
}
