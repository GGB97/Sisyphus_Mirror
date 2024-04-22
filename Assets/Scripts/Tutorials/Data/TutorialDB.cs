using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDB
{
    private Dictionary<int, TutorialData> _tutorials = new Dictionary<int, TutorialData>();
    //private List<int> idList = new List<int>(); //변경점

    public TutorialDB()
    {
        var res = Resources.Load<TutorialDB_Sheet>("Tutorials/Data/TutorialDB_Sheet");
        var tutorialSO = Object.Instantiate(res);
        var entities = tutorialSO.Entities;

        if (entities == null || entities.Count <= 0) return;

        var entityCount = entities.Count;

        for (int i = 0; i < entityCount; i++)
        {
            var tutorial = entities[i];

            if (_tutorials.ContainsKey(tutorial.Id)) _tutorials[tutorial.Id] = tutorial;
            else
            {
                _tutorials.Add(tutorial.Id, tutorial);
                //idList.Add(consumable.Id); //변경점
            }
        }
    }

    public TutorialData Get(int id)
    {
        if (_tutorials.ContainsKey(id)) return _tutorials[id];

        return null;
    }
    //public int GetItemId(int index) //변경점
    //{
    //    if (index < 0 && index < idList.Count) //index가 유효하지 않다면
    //        return 9999;

    //    return idList[index];
    //}
    //public int GetItemIdCount()//변경점
    //{
    //    return idList.Count;
    //}

    //public List<int> ReturnAllConsumableID()
    //{
    //    return idList;
    //}

    public IEnumerator TutorialEnumerator()
    {
        return _tutorials.GetEnumerator();
    }
}
