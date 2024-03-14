using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPoolManager Instance;

    [SerializeField] Pool[] _pools;
    public List<GameObject> _spawnObjects;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _spawnObjects = new List<GameObject>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 미리 생성
        foreach (Pool pool in _pools)
        {
            poolDictionary.Add(pool.tag, new Queue<GameObject>());
            for (int i = 0; i < pool.size; ++i)
            {
                var obj = CreateNewObject(pool.tag, pool.prefab);
                ArrangePool(obj);
            }
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        // 큐에 없으면 새로 추가
        Queue<GameObject> poolQueue = poolDictionary[tag];
        if (poolQueue.Count <= 0)
        {
            // tag로 pools에서 prefab 찾기
            Pool pool = Array.Find(_pools, x => x.tag == tag);
            var obj = CreateNewObject(pool.tag, pool.prefab);
            // ObjectPool 정렬시키기
            ArrangePool(obj);
        }

        // 큐에서 꺼내서 사용
        GameObject objectToSpawn = poolQueue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    GameObject CreateNewObject(string tag, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false); // 비활성화시 ReturnToPool을 하므로 Enqueue가 됨
        return obj;
    }

    void ArrangePool(GameObject obj)
    {
        // 추가된 오브젝트 묶어서 정렬
        bool isFind = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == transform.childCount - 1)
            {
                obj.transform.SetSiblingIndex(i);
                _spawnObjects.Insert(i, obj);
                break;
            }
            else if (transform.GetChild(i).name == obj.name)
                isFind = true;
            else if (isFind)
            {
                obj.transform.SetSiblingIndex(i);
                _spawnObjects.Insert(i, obj);
                break;
            }
        }
    }

    // 생성된 프리팹에서 Disable될 때 호출됨
    public void ReturnToPull(GameObject obj)
    {
        poolDictionary[obj.name].Enqueue(obj);
    }
}
