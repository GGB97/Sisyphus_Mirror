using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance; // 필요하지 않게될 수 있음.
    public Transform target; // 나중에 GameManager 같은데서 들고있으면 거기서 가져오게 하면 될듯

    private void Awake()
    {
        Instance = this;
    }
}
