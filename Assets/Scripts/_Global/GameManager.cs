using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameover = false;
    [SerializeField] private int _playerID;
    [SerializeField] private Player _player;

    public GameObject gameoverUI;

    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI goldText;

    public int currentFloor;
    public int currentLevel;
    public int killenemys;
    public int totalGold;


    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindAnyObjectByType<Player>();
                if(_player == null)
                    _player = Instantiate(Resources.Load<Player>(DataBase.Player.Get(_playerID).prefabPath));
            }
            return _player;
        }
        private set { _player = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isGameover)
        {
            Gameover();
        }
    }

    public void Gameover()
    {
        Time.timeScale = 0;
        DungeonManager.Instance.isStarted = false;

        EnemySpawner.Instance.SpawnStop();
        EnemySpawner.Instance.FindAllEnemiesDeSpawn();

        gameoverUI.SetActive(true);

        currentFloor = DungeonManager.Instance.currnetstage;
        currentLevel = Player.Data.LV;

        floorText.text = currentFloor.ToString();
        levelText.text = currentLevel.ToString();
        killText.text = killenemys.ToString();
        goldText.text = totalGold.ToString();
    }

    public void SetPlayer(Player newPlayer)
    {
        Player = newPlayer;
        _playerID = Player.Data.id;
        InventoryStats.Instance?.UpdateStatsPanel();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        isGameover = false;
        SceneManager.LoadScene(1);
        killenemys = 0;
        totalGold = 0;
    }
}
