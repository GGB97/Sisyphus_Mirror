using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuest : MonoBehaviour
{
    public QuestType type;
    public int target;
    public int count;
    // Start is called before the first frame update
    private void Start()
    {
        QuestManager.Instance.StartQuestSetting();
    }

    public void TestUpdate()
    {
        QuestManager.Instance.NotifyQuest(type,target,count);
    }
    public void Print()
    {
        QuestManager.Instance.PrintCurrentQuestList();
    }
}
