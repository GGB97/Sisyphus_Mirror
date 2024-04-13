using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject option;
    public void StartButton(Button btn)
    {
        GameManager.Instance.LoadScene(SceneName.Lobby);
        btn.enabled = false;
    }

    public void OptionButton()
    {
        option.SetActive(true);
    }

    public void GameExitButton()
    {
        Application.Quit();
    }

}
