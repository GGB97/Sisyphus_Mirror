using System;

namespace Constants
{
    [Serializable]
    public enum QuestType
    {
        KillMonster,
        Equip,
        StageClear,
        UseGold,
        AddBlock
    }

    [Serializable]
    public enum QuestState
    {
        Wait,
        Progress,
        Complete
    }
}

