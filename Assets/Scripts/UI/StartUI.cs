using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour , ClickButton
{
    [SerializeField]
    private string startSound = "ClickButton";
    [SerializeField] private GameObject option;
    public void StartButton(Button btn)
    {
        GameManager.Instance.LoadScene(SceneName.Lobby);
        btn.enabled = false;
        PlaySound();
        SceneManager.LoadScene(1);
    }

    public void OptionButton()
    {
        option.SetActive(true);
    }

    public void GameExitButton()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        SoundManager.Instance.PlayAudioClip(startSound);
    }
}
