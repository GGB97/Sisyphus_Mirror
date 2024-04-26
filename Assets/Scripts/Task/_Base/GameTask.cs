using UnityEngine;

[System.Serializable]
public class GameTask
{
    [SerializeField] protected int _id;
    [SerializeField] protected string _name;
    [SerializeField] protected string _desc;
    [SerializeField] protected TaskType _type;
    [SerializeField] protected int _target;
    [SerializeField] protected int _count;

    public int ID => _id;
    public string Name => _name;
    public string Desc => _desc;
    public TaskType Type => _type;
    public int Target => _target;
    public virtual int Count => _count;
}
