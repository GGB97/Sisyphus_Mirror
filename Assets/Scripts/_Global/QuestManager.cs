using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletoneBase<QuestManager>
{
    private Dictionary<int, Quest> _ongoingQuests = new Dictionary<int, Quest>();//현재 진행중인 퀘스트 집합
    private HashSet<int> _completeQuests = new HashSet<int>();//완료한 퀘스트의 id 집합
    private Dictionary<QuestType, List<QuestData>> _subscribeQuests = new Dictionary<QuestType, List<QuestData>>();//퀘스트 참조 베이스 데이터 집합

    public event Action<int> OnQuestStartCallback;//콜백
    public event Action<int, int> OnQuestUpdateCallback;
    public event Action<int> OnQuestClearCallback;

    private int[] startQuestId = new int[] { 100, 200 };//시작할 때 등록할 퀘스트들
    public List<int> dailyQuestList = new List<int>() { 310, 320, 330, 340, 350 };

    private void OnEnable()//꺼질일이 없어서 처음 들어올 때만 실행 로드하기 전
    {
        FieldInit();
    }
    private void Start()
    {
        InvokeRepeating("StartDailyQuest", GetTimeUntilMidnight(), 86400);// 인보크 리피팅이 다시 호출되어도 덮어써서 괜찮다고 한다.게임을 껐다 켜도 적용되는 지는 아직 잘 모르겠다.
    }
    public void SubsrcipbeQuest(int questId)//참조 등록
    {
        var questData = DataBase.Quest.Get(questId);//퀘스트 목록에서 id에 해당하는 퀘스트 정보 가져옴

        if (_subscribeQuests.ContainsKey(questData.Type) == false)//구독한 퀘스트 사전에 타입이 존재하는지 확인
            _subscribeQuests[questData.Type] = new List<QuestData>();//해당 타입이 없으면 새로운 List 생성

        _subscribeQuests[questData.Type].Add(questData);//해당하는 타입에 퀘스트 정보 추가
    }
    public void UnsubscribeQuest(int questId)//참조 해제
    {
        var questData = DataBase.Quest.Get(questId);//id에 해당하는 퀘스트 정보 가져옴

        if (_subscribeQuests.ContainsKey(questData.Type) == false)//해당하는 타입이 없다면 리턴
            return;

        _subscribeQuests[questData.Type].Remove(questData);//해당하는 타입 List 에서 해당 퀘스트 정보 삭제
    }
    public void NotifyQuest(QuestType type, int target, int count)//어떤 퀘스트 , 누구를 잡았고 ,얼만큼 실행했는지
    {
        if (_subscribeQuests.ContainsKey(type) == false)//구독한 것 중에서 타입이 없다면 리턴
            return;

        var filteredQuest = _subscribeQuests[type];//해당 타입 리스트 받아 오기
        var targetQuest = filteredQuest.FindAll(q => q.Target == target);//리스트에서 타겟이 일치하는 것을 찾아서 리스트에 담는다.
        foreach (var quest in targetQuest)//하나 씩 꺼내서
        {
            QuestUpdate(quest.Id, count);//id 에 해당하는 퀘스트를 count만큼 업데이트 한다.
        }
    }
    public void QuestStart(int questId, int progress = 0)//퀘스트 시작
    {
        if (IsClear(questId))//해당 id 퀘스트가 클리어인지 확인 = 이미 클리어 한 퀘스트인지 확인
            return;

        if (_ongoingQuests.ContainsKey(questId))//진행 중인 사전에 id에 해당하는 퀘스트가 있는지 확인
        {
            //Debug.Log("퀘스트 진행중");
            return;//이미 있으면 리턴
        }

        var quest = new Quest(questId, progress);//id에 해당하는 새로운 퀘스트 생성
        quest.Start();//상태 시작으로 변경

        _ongoingQuests.Add(questId, quest);//사전에 퀘스트 추가.

        SubsrcipbeQuest(questId);//구독에 참조할 데이터 넣음

        Debug.Log($"퀘스트 시작 : {DataBase.Quest.Get(questId).Name}");

        OnQuestStartCallback?.Invoke(questId);
    }
    public void QuestUpdate(int questId, int amount)//퀘스트 업데이트
    {
        if (_ongoingQuests.ContainsKey(questId) == false)//진행 중인 사전에 존재하는지 확인
            return;

        var questData = DataBase.Quest.Get(questId);//id에 해당하는 퀘스트를 가져옴 

        int currentCount = _ongoingQuests[questId].Update(amount);//진행 중인 퀘스트를 count만큼 업데이트

        OnQuestUpdateCallback?.Invoke(questId, amount);

        if (currentCount >= questData.Count)//퀘스트 정보와 비교하여 넘으면 퀘스트 클리어
            QuestClear(questId);
    }
    public void QuestClear(int questId)//퀘스트 클리어
    {
        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        if (CheckNextQuest(questId))//다음 퀘스트가 있는지 체크
        {//있다면
            int progress = _ongoingQuests[questId].QuestProgress;//진행도 유지
            QuestStart(questId + 1, progress);//다음 퀘스트 등록
        }

        _ongoingQuests[questId].Complete();//상태를 클리어 상태로 전환
        _ongoingQuests.Remove(questId);//제거

        UnsubscribeQuest(questId);//참조에서 빼기
        _completeQuests.Add(questId);//완료에 넣음

        Debug.Log($"퀘스트 완료 : {DataBase.Quest.Get(questId).Name}");

        OnQuestClearCallback?.Invoke(questId);//완료 후 호출 되는 것들 
    }

    public bool IsClear(int id)//클리어에 있는지 확인
    {
        return _completeQuests.Contains(id);
    }
    public void StartQuestSetting()//처음 시작했을 때 기본 퀘스트 등록 일일 퀘스트 제외
    {
        foreach (var id in startQuestId)//시작 시 기본 퀘스트들 자동 등록
        {
            QuestStart(id);
        }
        StartDailyQuest();//일일 퀘스트 등록
    }
    public void StartDailyQuest()//일일 퀘스트 시작 자정될 때 호출
    {
        foreach (var questId in dailyQuestList)//일일 퀘스트 목록
        {
            if (_ongoingQuests.ContainsKey(questId) == true)//onGoing에 있다면 진행 중인거니
            {
                _ongoingQuests[questId].ResetQuest();//진행도 0 및 진행으로 변경
            }
            else if (_completeQuests.Contains(questId) == true)//클리어 한 퀘스트라면 보상 받았는지 여부X
            {
                _completeQuests.Remove(questId); //클리어 목록에서 제거
                QuestStart(questId);//퀘스트 다시 시작
            }
            else//처음 퀘스트를 받는 거라면
            {
                QuestStart(questId);
            }
        }
    }
    public void PrintCurrentQuestList()
    {
        Debug.Log($"현재 진행 중인 퀘스트 목록 : ");
        foreach (var questList in _subscribeQuests)
        {
            foreach (var questId in questList.Value)
                Debug.Log($"{questId.Name}");
        }
    }

    public bool CheckNextQuest(int questId)
    {
        if (DataBase.Quest.Get(questId + 1) == null)
            return false;
        else
            return true;
    }
    public void LoadData(QuestSaveData questSaveData)//데이터 로드
    {
        foreach (var quest in questSaveData.ongoingQuests)
        {
            QuestStart(quest.Key, quest.Value);
        }
        _completeQuests = questSaveData.completeQuests;
    }
    public QuestSaveData SaveData()//데이터 저장
    {
        QuestSaveData questSaveData = new QuestSaveData();
        foreach (var quest in _ongoingQuests)
        {
            var questData = DataBase.Quest.Get(quest.Key);
            if (questData.IsStorable == true)
            {
                questSaveData.ongoingQuests[quest.Key] = quest.Value.QuestProgress;//id와 진행상황 저장
            }
            else
            {
                questSaveData.ongoingQuests[quest.Key] = 0;
            }
        }
        questSaveData.completeQuests = _completeQuests;

        return questSaveData;
    }
    public void FieldInit()
    {
        //Debug.Log("퀘스트 필드 초기화");
        _ongoingQuests.Clear();
        _completeQuests.Clear();
        _subscribeQuests.Clear();
    }
    public int? CheckQuestProgress(int questId)//퀘스트 진행도 체크
    {
        if (_ongoingQuests[questId] != null)//진행 중인 퀘스트라면 
            return _ongoingQuests[questId].QuestProgress;

        if (_completeQuests.Contains(questId) == true)//클리어 한 퀘스트라면
        {
            QuestData questData = DataBase.Quest.Get(questId);
            return questData.Count;
        }

        return null;
    }
    public float GetTimeUntilMidnight()//현재 시간부터 자정까지의 시간 반환
    {
        System.DateTime now = System.DateTime.Now;
        System.DateTime tomorrow = now.AddDays(1).Date;
        System.TimeSpan timeUntilMidnight = tomorrow - now;
        Debug.Log($"리셋까지 남은 시간(초) : {(float)timeUntilMidnight.TotalSeconds}");
        return (float)timeUntilMidnight.TotalSeconds;
    }
}
