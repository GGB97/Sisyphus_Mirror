using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        //PlayerPrefs.SetInt("Intro", 0); // Test용
        int introCheck = PlayerPrefs.GetInt("Intro");

        if (introCheck == 1) // 처음이 아니라면 기존 씬전환으로
        {
            GameManager.Instance.LoadScene(SceneName.Lobby);
        }
        else if (introCheck == 0) // 처음이라면 인트로 재생 후 씬 전환으로
        {
            UIManager.Instance.FadeOut(0.5f, () =>
            {
                StartCoroutine(LoadAsyncSceneIntro(SceneName.Lobby, "UI/Intro/IntroCanvas"));
            });
        }
    }

    IEnumerator LoadAsyncSceneIntro(string sceneName, string introPath)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // 캔버스 생성
        Intro intro = Instantiate(Resources.Load(introPath)).GetComponent<Intro>();
        intro.loadSceneAction += () =>
        {
            PlayerPrefs.SetInt("Intro", 1);
            asyncLoad.allowSceneActivation = true;
        };

        while (asyncLoad.progress < 0.9f)
        {
            //Debug.Log("loading" + asyncLoad.progress); // 씬 로딩이 매우 짧아서 진행상황이 한번에 끝남..
            intro.UpdateFill(asyncLoad.progress * (1 / 0.9f));

            yield return null;
        }

        //마지막에 갱신 한번 더
        //Debug.Log("complete" + asyncLoad.progress);
        intro.UpdateFill(asyncLoad.progress * (1 / 0.9f));
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
