using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Surface : MonoBehaviour
{
    NavMeshSurface _nms;

    private void Awake()
    {
        _nms = GetComponent<NavMeshSurface>();
    }

    void Start()
    {
        _nms.BuildNavMesh();
    }
}
