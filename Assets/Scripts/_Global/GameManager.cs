using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    [SerializeField] private int _playerID = 20000002;
    [SerializeField] private Player _player;

    public GameState gameState;

    public GameOverUI GameOverUI;
    public Menu Menu;

    public int killenemys;
    public int totalGold;

    public Action onGameOverEvent;

    [SerializeField] int _tutorialId = 60003011;

    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindAnyObjectByType<Player>();
                if (_player == null)
                {
                    _player = Instantiate(Resources.Load<Player>(DataBase.Player.Get(_playerID).prefabPath));
                }
            }
            return _player;
        }
        private set { _player = value; }
    }

    public override void Init()
    {
        gameState = GameState.Lobby;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OpenMenu()
    {
        if (UIManager.Instance.CheckActiveUI())
        {
            Menu.OpenMenu();
        }

        //if (Menu.gameObject.activeSelf == true)
        //{

        //    Menu.CloseUI();
        //}
        //else
        //{
            
        //    Menu.OpenMenu();
        //}
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // 비동기 로드를 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 씬의 활성화를 막음

        // allowSceneActivation == false일 때 0.9까지만 진행
        while (asyncLoad.progress < 0.9f)
        {
            //Debug.Log("Loading : " + asyncLoad.progress);
            yield return null;
        }

        // asyncLoad.progress < 0.9f == true라면 씬의 대부분의 데이터가 이미 메모리에 로드되어있는 상태
        // 0.9에서 1.0으로 이동하는 단계는 씬의 "활성화" 단계라서 빠르게 처리됨.
        // ex) 총 로딩 시간이 100초인 경우 = 0.9 ~ 1.0 이 걸리는 시간은 약 1~2초 이내로 예상됨.
        UIManager.Instance.FadeOut(0.5f, () =>
        {
            asyncLoad.allowSceneActivation = true; // 페이드가 완료되면 씬이 전환 가능하게 변경.
        });
    }

    public void Gameover()
    {
        DungeonManager.Instance.gameState = DungeonState.Fail;

        EnemySpawner.Instance.SpawnStop();

        PrintPlayData();
        onGameOverEvent?.Invoke();

        GameOverUI.Show();
    }

    public void SetPlayer(Player newPlayer)
    {
        Player = newPlayer;
        _playerID = Player.Data.id;
        InventoryStats.Instance?.UpdateStatsPanel();
    }

    public void Retry()
    {
        QuestSaveManager.Instance.SaveData();
        QuestManager.Instance.FieldInit();
        Destroy(DungeonManager.Instance.gameObject);

        _player.playerReset();

        killenemys = 0;
        totalGold = 0;

        gameState = GameState.Lobby;
        LoadScene(SceneName.Lobby);
    }

    void PrintPlayData()
    {
        Debug.Log("-----------이번 던전의 플레이 데이터-----------");
        Debug.Log($"현재 층 수 : {DungeonManager.Instance.currnetstage}층");
        Debug.Log($"던전 종료 시 플레이어 레벨 : {Player.Data.LV}");
        Debug.Log($"총 잡은 몬스터 수 : {killenemys}");
        Debug.Log($"총 얻은 골드 량 : {totalGold}");

        Debug.Log($"현재 소지 중인 룬 조각 : {PlayerPrefs.GetInt("Rune")}");
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIManager.Instance.FadeIn(0.5f);

        if (scene.buildIndex == 1 && PlayerPrefs.GetInt("dungeonStartTutorialFlag") == 0)
        {
            TutorialManager.Instance.PopupTutorial(TutorialType.DungeonStart, _tutorialId);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        onGameOverEvent?.Invoke();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
