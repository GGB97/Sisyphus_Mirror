using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private QuestType _type;
    [SerializeField] private int _target;
    [SerializeField] private int _count;
    [SerializeField] private int _exp;
    [SerializeField] private int _gold;

    [SerializeField] private int _reward_1;
    [SerializeField] private int _rewardCount_1;
    [SerializeField] private int _reward_2;
    [SerializeField] private int _rewardCount_2;
    [SerializeField] private int _reward_3;
    [SerializeField] private int _rewardCount_3;
    [SerializeField] private bool isStorable;

    public int Id { get => _id; }
    public string Name { get => _name; }
    public string Description { get => _description; }
    public QuestType Type { get => _type; }
    public int Target { get => _target; }
    public int Count { get => _count; }
    public int Exp { get => _exp; }
    public int Gold { get => _gold; }
    public bool IsStorable { get => isStorable; }

    private List<Reward> _rewardList;
    public List<Reward> RewardList
    {
        get
        {
            if (_rewardList == null)
            {
                _rewardList = new List<Reward>();
                CheckReward(_reward_1, _rewardCount_1);
                CheckReward(_reward_2, _rewardCount_2);
                CheckReward(_reward_3, _rewardCount_3);
            }

            return _rewardList;
        }
    }

    private void CheckReward(int rewardId, int rewardCount)
    {
        if (rewardId != 0)
            _rewardList.Add(new Reward(rewardId, rewardCount));
    }
}
[Serializable]
public class Reward
{
    public int reward { get; }
    public int rewardCount { get; }
    public Reward(int rewardId, int rewardCount)
    {
        reward = rewardId;
        this.rewardCount = rewardCount;
    }
}
