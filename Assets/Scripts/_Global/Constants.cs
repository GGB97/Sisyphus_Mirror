using System;

namespace Constants
{
    [Serializable]
    public enum QuestType
    {
        KillMonster,
        GetItem,
        StageClear
    }

    [Serializable]
    public enum QuestState
    {
        Wait,
        Progress,
        Complete
    }
}

