using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestManager : SingletoneBase<QuestManager>
{
    private Dictionary<int, Quest> _ongoingQuests = new Dictionary<int, Quest>();//현재 진행중인 퀘스트 집합
    private HashSet<int> _completeQuests = new HashSet<int>();//완료한 퀘스트의 id 집합
    private Dictionary<QuestType, List<QuestData>> _subscribeQuests = new Dictionary<QuestType, List<QuestData>>();//퀘스트 참조 베이스 데이터 집합

    public event Action<int> OnQuestStartCallback;//콜백
    public event Action<int,int> OnQuestUpdateCallback;
    public event Action<int> OnQuestClearCallback;

    private int[] startQuestId = new int[] {100,200};//시작할 때 등록할 퀘스트들

    public void SubsrcipbeQuest(int questId)//등록
    {
        var questData = DataBase.Quest.Get(questId);//퀘스트 목록에서 id에 해당하는 퀘스트 정보 가져옴

        if (_subscribeQuests.ContainsKey(questData.Type)==false)//구독한 퀘스트 사전에 타입이 존재하는지 확인
            _subscribeQuests[questData.Type] = new List<QuestData>();//해당 타입이 없으면 새로운 List 생성

        _subscribeQuests[questData.Type].Add(questData);//해당하는 타입에 퀘스트 정보 추가
    }
    public void UnsubscribeQuest(int questId)//해제
    {
        var questData = DataBase.Quest.Get(questId);//id에 해당하는 퀘스트 정보 가져옴

        if (_subscribeQuests.ContainsKey(questData.Type) == false)//해당하는 타입이 없다면 리턴
            return;

        _subscribeQuests[questData.Type].Remove(questData);//해당하는 타입 List 에서 해당 퀘스트 정보 삭제
    }
    public void NotifyQuest(QuestType type,int target,int count)//어떤 퀘스트 , 누구를 잡았고 ,얼만큼 실  행했는지
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
    public void QuestStart(int questId)//퀘스트 시작
    {
        if (IsClear(questId))//해당 id 퀘스트가 클리어인지 확인 = 이미 클리어 한 퀘스트인지 확인
            return;

        var quest = new Quest(questId);//id에 해당하는 새로운 퀘스트 생성
        quest.Start();//상태 시작으로 변경

        if (_ongoingQuests.ContainsKey(questId))//진행 중인 사전에 id에 해당하는 퀘스트가 있는지 확인
            return;//이미 있으면 리턴

        _ongoingQuests.Add(questId, quest);//사전에 퀘스트 추가.

        SubsrcipbeQuest(questId);//구독에 참조할 데이터 넣음

        Debug.Log($"퀘스트 시작 : {DataBase.Quest.Get(questId).Name}");

        OnQuestStartCallback?.Invoke(questId);
    }
    public void QuestUpdate(int questId,int amount)//퀘스트 업데이트
    {
        if (_ongoingQuests.ContainsKey(questId) == false)//진행 중인 사전에 존재하는지 확인
            return;

        var questData = DataBase.Quest.Get(questId);//id에 해당하는 퀘스트를 가져옴 

        int currentCount =  _ongoingQuests[questId].Update(amount);//진행 중인 퀘스트를 count만큼 업데이트

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
            QuestStart(questId + 1);//다음 퀘스트 등록
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
    public void StartQuestSetting()//처음 시작했을 때 기본 퀘스트 등록
    {
        foreach (var id in startQuestId)//시작 시 기본 퀘스트들 자동 등록
        {
            QuestStart(id);
        }
    }
    public void PrintCurrentQuestList()
    {
        Debug.Log($"현재 진행 중인 퀘스트 목록 : ");
        foreach (var questList in _subscribeQuests)
        {
            foreach(var questId in questList.Value)
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
}
