using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using static ObjectPoolManager;

public class Surface : MonoBehaviour
{
    public EnemySize[] enemyTypes;
    NavMeshSurface[] _nms;

    private void Awake()
    {
        _nms = new NavMeshSurface[enemyTypes.Length];
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            _nms[i] = gameObject.AddComponent<NavMeshSurface>();
            _nms[i].agentTypeID = NavMesh.GetSettingsByIndex((int)enemyTypes[i]).agentTypeID; // AgentType을 가져옴
            LayerMask includeLayer = LayerData.Terrain | LayerData.Water; // bake할 레이어
            _nms[i].layerMask = includeLayer;
        }
    }

    void Start()
    {
        foreach(var nms in _nms)
        {
            nms.BuildNavMesh(); // Bake
        }

        //GameObject go = Resources.Load<GameObject>("Projectile/Prefabs/Orange Explosion");
        //Instantiate(go);
    }
}
