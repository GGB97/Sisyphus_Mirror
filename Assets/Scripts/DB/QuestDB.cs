using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDB
{
    private Dictionary<int, QuestData> _quest = new();

    public QuestDB()
    {
        var res = Resources.Load<QuestSO>(DBPath.QuestDB);
        var questSo = Object.Instantiate(res);
        var entities = questSo.Sheet1;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var quest = entities[i];

            if (_quest.ContainsKey(quest.Id))
                _quest[quest.Id] = quest;
            else
                _quest.Add(quest.Id, quest);
        }
    }

    public QuestData Get(int id)
    {
        if (_quest.ContainsKey(id))
            return _quest[id];


        return null;
    }

    public IEnumerator DbEnumerator()
    {
        return _quest.GetEnumerator();
    }
}
