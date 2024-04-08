using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum QuestType
    {
        KillMonster,
        GetItem,
        StageClear
    }

    public enum QuestState
    {
        Wait,
        Progress,
        Complete    
    }
}

