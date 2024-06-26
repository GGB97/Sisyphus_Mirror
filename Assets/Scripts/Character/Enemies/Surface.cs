using Unity.AI.Navigation;
using UnityEngine;

public class Surface : MonoBehaviour
{
    public EnemySize[] enemyTypes;
    NavMeshSurface[] _nms;

    private void Awake()
    {
        _nms = new NavMeshSurface[enemyTypes.Length];
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            //_nms[i] = gameObject.AddComponent<NavMeshSurface>();
            //_nms[i].agentTypeID = NavMesh.GetSettingsByIndex((int)enemyTypes[i]).agentTypeID; // AgentType을 가져옴
            //LayerMask includeLayer = LayerData.Terrain; // bake할 레이어
            //_nms[i].layerMask = includeLayer;
        }

        foreach (var nms in _nms)
        {
            //nms.BuildNavMesh(); // Bake
        }
    }
}
