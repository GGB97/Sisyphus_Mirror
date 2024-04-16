using Constants;
using System;
using UnityEngine;

[Serializable]
public class Quest
{
    [SerializeField] private int _questId;
    [SerializeField] private int _questProgress;
    [SerializeField] private QuestState _questState;

    public int QuestId { get => _questId; private set => _questId = value; }
    public int QuestProgress { get => _questProgress; private set => _questProgress = value; }
    public QuestState QuestState { get => _questState; private set => _questState = value; }
    public Quest(int questId)//id만으로 생성할 때
    {
        _questId = questId;
        _questProgress = 0;
        _questState = QuestState.Wait;
    }
    public Quest(int questId, int questProgress = 0, QuestState questState = QuestState.Wait)
    {
        _questId = questId;
        _questProgress = questProgress;
        _questState = questState;
    }
    public void Start()
    {
        _questState = QuestState.Progress;
    }
    public int Update(int amount)
    {
        _questProgress += amount;
        return _questProgress;
    }

    public void Complete()
    {
        _questState = QuestState.Complete;
    }
    public void ProgressClear()
    {
        _questProgress = 0;
    }
}
