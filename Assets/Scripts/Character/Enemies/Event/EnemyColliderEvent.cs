using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderEvent : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        enemy.OnChildTriggerEnter(other);
    }
}
