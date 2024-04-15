using UnityEngine;

public class DungeonGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            GameManager.Instance.gameState = GameState.Dungeon;
            GameManager.Instance.LoadScene(SceneName.Dungeon);
        }
    }
}
