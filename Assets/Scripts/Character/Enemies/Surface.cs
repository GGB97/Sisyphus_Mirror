using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Surface : MonoBehaviour
{
    public EnemyType[] enemyTypes;
    NavMeshSurface[] _nms;

    private void Awake()
    {
        _nms = new NavMeshSurface[enemyTypes.Length];
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            _nms[i] = gameObject.AddComponent<NavMeshSurface>();
            _nms[i].agentTypeID = NavMesh.GetSettingsByIndex((int)enemyTypes[i]).agentTypeID;
            LayerMask includeLayer = LayerData.Default | LayerData.Water; // 맵 구성되고 변경 예정
            _nms[i].layerMask = includeLayer;
        }
    }

    void Start()
    {
        foreach(var nms in _nms)
        {
            nms.BuildNavMesh();
        }
    }
}
