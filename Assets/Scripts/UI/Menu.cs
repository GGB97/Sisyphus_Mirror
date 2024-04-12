using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject option;
    public void Continue()
    {
        gameObject.SetActive(false);
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
