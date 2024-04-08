using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletoneBase<QuestManager>
{
    private Dictionary<int, Quest> _ongoingQuests = new Dictionary<int, Quest>();
    private HashSet<int> _completeQuests = new HashSet<int>();

    public event Action<int> OnQuestStartCallback;
    public event Action<int,int> OnQuestUpdateCallback;
    public event Action<int> OnQuestClearCallback;

    private Dictionary<QuestType, List<QuestData>> _subscribeQuests = new Dictionary<QuestType, List<QuestData>>();

    public void SubsrcipbeQuest(int questId)//등록
    {
        var questData = DataBase.Quest.Get(questId);

        if (_subscribeQuests.ContainsKey(questData.Type))
            _subscribeQuests[questData.Type] = new List<QuestData>();

        _subscribeQuests[questData.Type].Add(questData);
    }
    public void UnsubscribeQuest(int questId)//해제
    {
        var questData = DataBase.Quest.Get(questId);

        if (_subscribeQuests.ContainsKey(questData.Type) == false)
            return;

        _subscribeQuests[questData.Type].Remove(questData);
    }
    public void NotifyQuest(QuestType type,int target,int count)//어떤 퀘스트 , 누구를 잡았고 ,얼만큼 실행했는지
    {
        if (_subscribeQuests.ContainsKey(type) == false)
            return;

        var filteredQuest = _subscribeQuests[type];//리스트 받아오기
        var targetQuest = filteredQuest.FindAll(q => q.Target == target);
        foreach (var quest in targetQuest)
        {
            QuestUpdate(quest.Id, count);
        }
    }
    public void QuestStart(int questId)
    {
        if (IsClear(questId))
            return;

        var quest = new Quest(questId);
        quest.Start();

        if (_ongoingQuests.ContainsKey(questId))
            return;

        _ongoingQuests.Add(questId, quest);

        SubsrcipbeQuest(questId);

        OnQuestStartCallback?.Invoke(questId);
    }
    public void QuestUpdate(int questId,int amount)
    {
        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        var questData = DataBase.Quest.Get(questId);

        int currentCount =  _ongoingQuests[questId].Update(amount);

        OnQuestUpdateCallback?.Invoke(questId, amount);

        if (currentCount > questData.Count)
            QuestClear(questId);
    }
    public void QuestClear(int questId)
    {
        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        _ongoingQuests[questId].Complete();//상태를 클리어 상태로 전환
        _ongoingQuests.Remove(questId);

        _completeQuests.Add(questId);

        OnQuestClearCallback?.Invoke(questId);
    }

    public bool IsClear(int id)
    { 
        return _completeQuests.Contains(id);
    }
}
