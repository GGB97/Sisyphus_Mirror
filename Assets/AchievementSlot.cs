using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    public int questId;//퀘스트 아이디
    public Sprite icon;//아이콘
    [SerializeField]
    public TextMeshProUGUI questDescription;//퀘스트 설명
    [SerializeField]
    public TextMeshProUGUI rewardText;//보상 텍스트
    [SerializeField]
    public TextMeshProUGUI progressText;//진행도 텍스트
    [SerializeField]
    public Button rewardButton;//보상 버튼

    public void Init(int questId)//초기화
    {
        this.questId = questId;
        QuestData questData = DataBase.Quest.Get(questId);

        if(questData == null)//퀘스트 정보가 없으면
            Destroy(gameObject);

        QuestManager.Instance.QuestStart(questId);//퀘스트 시작
        questDescription.text = questData.Description;//퀘스트 설명
        StringBuilder sb = new StringBuilder(50);
        sb.Append("보상 : ");
        for (int i = 0; i < questData.RewardList.Count; i++)//보상 설정
        {
            sb.Append($"{questData.RewardList[i].reward} * {questData.RewardList[i].rewardCount} ");
        }
        rewardText.text = sb.ToString();
        sb = null;

        int? value = QuestManager.Instance.CheckQuestProgress(questId).Value;
        if(value != null)//널이 아닐 때만
            progressText.text = string.Format($"({value} / {questData.Count})");//진행도 설정

    }
    public void SlotUpdate()
    {
        int? value = QuestManager.Instance.CheckQuestProgress(questId).Value;

        if (value != null)//널이 아닐 때만
        {
            QuestData questData = DataBase.Quest.Get(questId);
            progressText.text = string.Format($"({value} / {questData.Count})");//진행도 설정
        }
        else
        {
            progressText.text = string.Empty;
        }
    }
}
