using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject option;

    private void Awake()
    {
        GameManager.Instance.Menu = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void Option()
    {
        option.SetActive(true);
    }

    public void GameEnd()
    {
        if(GameManager.Instance.gameState == GameState.Dungeon)
        {
            GameManager.Instance.Retry();
        }
        else
        {
            Application.Quit();
        }
    }
}
