using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : UI_Base
{
    UIManager _uiManager;
    QuestManager _questManager;

    [SerializeField]
    private GameObject achievementSlotPrefab;
    [SerializeField]
    private GameObject content1;
    [SerializeField]
    private GameObject content2;
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
    public void CreateDailySlot()
    {
        List<int> dailyQuestList = QuestManager.Instance.dailyQuestList;

        foreach (var questId in dailyQuestList) 
        {
            GameObject go = Instantiate(achievementSlotPrefab, content1.transform);
            AchievementSlot aSlot = go.GetComponent<AchievementSlot>();
            aSlot.Init(questId);
        }
    }
}
