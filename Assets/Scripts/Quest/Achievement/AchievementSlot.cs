using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using static System.Net.Mime.MediaTypeNames;

public class AchievementSlot : MonoBehaviour
{
    public int questId;//퀘스트 아이디
    private List<int> rewardAmount;
    public Sprite icon;//아이콘
    [SerializeField]
    public TextMeshProUGUI questDescription;//퀘스트 설명
    [SerializeField]
    public TextMeshProUGUI rewardText;//보상 텍스트
    [SerializeField]
    public TextMeshProUGUI progressText;//진행도 텍스트
    [SerializeField]
    public Button rewardButton;//보상 버튼
    [SerializeField]
    private string buttonSoundTag = "ItemGain";
    public void Init(int questId,QuestMode questMode)//초기화
    {
        this.questId = questId;
        rewardAmount = new List<int>();
        QuestData questData = DataBase.Quest.Get(questId);
        if (questData == null && questMode == questData.Mode)//퀘스트 정보가 없거나 모드가 일치하지 않으면 삭제
            Destroy(gameObject);

        questDescription.text = questData.Description;//퀘스트 설명
        StringBuilder sb = new StringBuilder(50);
        sb.Append("보상 : ");
        for (int i = 0; i < questData.RewardList.Count; i++)//보상 설정
        {
            rewardAmount.Add(questData.RewardList[i].rewardCount);//보상 카운트 저장
            sb.Append($"룬스톤 * {questData.RewardList[i].rewardCount} ");//보상 룬스톤으로 고정
        }
        rewardText.text = sb.ToString();
        sb = null;

        int? value = QuestManager.Instance.CheckQuestProgress(questId).Value;
        if (value != null)//널이 아닐 때만
        {
            ButtonIsActive((int)value, questData.Count);
            progressText.text = string.Format($"진행도 : ({value} / {questData.Count})");//진행도 설정
        }

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
    public void ButtonIsActive(int progress, int maxCount)
    {
        TextMeshProUGUI text = rewardButton.GetComponentInChildren<TextMeshProUGUI>();
        if (progress == maxCount)//완료되었는지
        {
            if(QuestManager.Instance.CheckReward(questId) == true)//보상 받을 수 있는지
            {
                rewardButton.interactable = true;
                text.text = "보상 받기";
            }
            else
            {
                rewardButton.interactable = false;
                text.text = "수령 완료";
            }
        }
        else
        {
            rewardButton.interactable = false;
            text.text = "진행 중";
        }
    }
    public void PressButton()
    {
        TextMeshProUGUI text = rewardButton.GetComponentInChildren<TextMeshProUGUI>();//텍스트
        text.text = "수령 완료";//텍스트 변경
        rewardButton.interactable = false;//버튼 비활성화

        QuestManager.Instance.ChangeRewardState(questId,false);//리워드 받은 처리
        SoundManager.Instance.PlayAudioClip(buttonSoundTag);//사운드 재생
        foreach (var amount in rewardAmount)//보상 수 만큼 지급
        {
            GameManager.Instance.Player.GetComponent<Player>().ChangeRune(amount);//플레이어에게 룬스톤 지급
        }

    }
}
