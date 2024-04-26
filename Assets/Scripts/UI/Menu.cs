using UnityEngine;

public class Menu : UI_Base
{
    UIManager _uiManager;
    [SerializeField] private GameObject option;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _uiManager = UIManager.Instance;
        GameManager.Instance.Menu = this;
    }

    private void OnDisable()
    {
        _uiManager.RemoveActiveUI(gameObject);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        _uiManager.AddActiveUI(gameObject);
        Time.timeScale = 0;
    }

    public override void CloseUI()
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
        if (GameManager.Instance.gameState == GameState.Dungeon)
        {
            GameManager.Instance.Retry();
        }
        else
        {
            Application.Quit();
        }
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
