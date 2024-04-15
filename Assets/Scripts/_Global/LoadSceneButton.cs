using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public static void LoadScene()
    {
        GameManager.Instance.LoadScene(SceneName.Lobby);
    }
}
