using UnityEngine;

public class DungeonGate : MonoBehaviour
{
    [SerializeField] private GameObject guide;
    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            GameManager.Instance.gameState = GameState.Dungeon;
            GameManager.Instance.ResetActiveID();
            GameManager.Instance.LoadScene(SceneName.Dungeon);
        }

        else
        {
            guide.SetActive(true);
            guide.transform.forward = Camera.main.transform.forward;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        guide?.SetActive(false);
    }
}
