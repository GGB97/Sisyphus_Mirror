using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemsPooler : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public string id;
        public int size;
    }

    public static FieldItemsPooler _instance;

    public static FieldItemsPooler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<FieldItemsPooler>();
                if (_instance == null)
                {
                    string typeName = typeof(FieldItemsPooler).FullName;
                    GameObject go = new GameObject(typeName);
                    _instance = go.AddComponent<FieldItemsPooler>();
                }
            }

            return _instance;
        }
    }

    [SerializeField] Pool[] _pools;
    public List<GameObject> _spawnObjects;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _spawnObjects = new List<GameObject>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 미리 생성
        foreach (Pool pool in _pools)
        {
            poolDictionary.Add(pool.id, new Queue<GameObject>());
            for (int i = 0; i < pool.size; ++i)
            {
                GameObject projectilePrefab = Resources.Load<GameObject>($"Items/Prefabs/Consumable/FieldItems/{pool.id}");
                var obj = CreateNewObject(pool.id, projectilePrefab);
                //ArrangePool(obj); // 실행하지 않아도 상관없음
            }
        }
    }

    public GameObject SpawnFromPool(string id, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(id))
            return null;

        // 큐에 없으면 새로 추가
        Queue<GameObject> poolQueue = poolDictionary[id];
        if (poolQueue.Count <= 0)
        {
            // tag로 pools에서 prefab 찾기
            Pool pool = Array.Find(_pools, x => x.id == id);
            GameObject projectilePrefab = Resources.Load<GameObject>($"Items/Prefabs/Consumable/FieldItems/{pool.id}");
            var obj = CreateNewObject(pool.id, projectilePrefab);
            // ObjectPool 정렬시키기
            //ArrangePool(obj); // 실행하지 않아도 상관없음
        }

        // 큐에서 꺼내서 사용
        GameObject objectToSpawn = poolQueue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    GameObject CreateNewObject(string id, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = id;
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
        string id = obj.name;
        if (poolDictionary.ContainsKey(id))
            poolDictionary[id].Enqueue(obj);
        else Destroy(obj);
    }
}
