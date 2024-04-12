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
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Option()
    {
        option.SetActive(true);
    }

    public void GameEnd()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(sceneIndex == 2)
        {
            GameManager.Instance.Retry();

        }
        else
            Application.Quit();
    }
}
