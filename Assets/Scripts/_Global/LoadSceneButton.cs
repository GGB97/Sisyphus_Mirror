using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public static void LoadScene()
    {
        GameManager.Instance.LoadScene(SceneName.Lobby);
    }
}
