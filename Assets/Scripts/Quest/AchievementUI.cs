using System.Collections.Generic;
using UnityEngine;

public class AchievementUI : UI_Base
{
    UIManager _uiManager;
    QuestManager _questManager;

    [SerializeField]
    private GameObject achievementSlotPrefab;
    [SerializeField]
    private GameObject questContent;
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
    public void CreateDailyQuest()
    {
        List<int> dailyQuestList = QuestManager.Instance.dailyQuestList;

        foreach (var questId in dailyQuestList)
        {
            GameObject go = Instantiate(achievementSlotPrefab, questContent.transform);
            AchievementSlot aSlot = go.GetComponent<AchievementSlot>();
            aSlot.Init(questId);
        }
    }
}
