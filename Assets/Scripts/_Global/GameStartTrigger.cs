using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    private void Start()
    {
        DungeonManager.Instance.OpenInventory();
        Destroy(this.gameObject);
    }
}
