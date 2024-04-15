using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour, ClickButton
{
    [SerializeField]
    private string startSound = "ClickButton";
    [SerializeField] private GameObject option;
    public void StartButton(Button btn)
    {
        btn.enabled = false;
        PlaySound();
        GameManager.Instance.LoadScene(SceneName.Lobby);
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
