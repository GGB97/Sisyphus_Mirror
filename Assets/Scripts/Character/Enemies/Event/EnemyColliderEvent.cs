using UnityEngine;

public class EnemyColliderEvent : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] SkillType type;
    private void OnTriggerEnter(Collider other)
    {
        enemy.OnChildTriggerEnter(other, type);
    }
}
