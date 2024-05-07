using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    #region Pool Struct 
    [Serializable]
    public struct Pool
    {
        public int id;
        public int size;
    }
    #endregion
    [SerializeField] Pool[] _pools; // (int id, int size)
    public List<GameObject> spawnObjects;
    public Dictionary<int, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawnObjects = new List<GameObject>();
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        // 미리 생성
        foreach (Pool pool in _pools)
        {
            poolDictionary.Add(pool.id, new Queue<GameObject>());
            for (int i = 0; i < pool.size; ++i)
            {
                GameObject projectilePrefab = Resources.Load<GameObject>(DataBase.Projectile.Get(pool.id).prefabPath);
                var obj = CreateNewObject(pool.id, projectilePrefab);
                //ArrangePool(obj); // 실행하지 않아도 상관없음
            }
        }
    } // 시작시 미리 정의된 풀링 갯수만큼 생성.

    public GameObject SpawnFromPool(int id, Vector3 position, Quaternion rotation) // objpool로 부터 외부에서 가져올때 사용하는 함수.
    {
        if (!poolDictionary.ContainsKey(id))
            return null;

        // 큐에 없으면 새로 추가
        Queue<GameObject> poolQueue = poolDictionary[id];
        if (poolQueue.Count <= 0)
        {
            // tag로 pools에서 prefab 찾기
            Pool pool = Array.Find(_pools, x => x.id == id);
            GameObject projectilePrefab = Resources.Load<GameObject>(DataBase.Projectile.Get(pool.id).prefabPath);
            var obj = CreateNewObject(pool.id, projectilePrefab);
            //ArrangePool(obj);
        }

        // 큐에서 꺼내서 사용
        GameObject objectToSpawn = poolQueue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    GameObject CreateNewObject(int id, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = id.ToString();
        obj.SetActive(false); // 비활성화시 ReturnToPool을 하므로 Enqueue가 됨
        return obj;
    }

    // 생성된 프리팹에서 Disable될 때 호출됨
    public void ReturnToPull(GameObject obj)
    {
        if (int.TryParse(obj.name, out int id))
            poolDictionary[id].Enqueue(obj);
        else Destroy(obj);
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
                spawnObjects.Insert(i, obj);
                break;
            }
            else if (transform.GetChild(i).name == obj.name)
                isFind = true;
            else if (isFind)
            {
                obj.transform.SetSiblingIndex(i);
                spawnObjects.Insert(i, obj);
                break;
            }
        }
    }

    public void DisableChild()
    {
        var projectiles = GetComponentsInChildren<GameObject>(false);

        foreach (var item in projectiles)
        {
            item.SetActive(false);
        }
    }
}
