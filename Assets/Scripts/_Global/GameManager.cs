using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    [SerializeField] private int _playerID;
    [SerializeField] private Player _player;

    public GameOverUI GameOverUI;

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
                {
                    _player = Instantiate(Resources.Load<Player>(DataBase.Player.Get(_playerID).prefabPath));
                }
            }
            return _player;
        }
        private set { _player = value; }
    }

    private void Awake()
    {
        GameOverUI = GameObject.FindObjectOfType<GameOverUI>();
    }
    public void Gameover()
    {
        DungeonManager.Instance.isStarted = false;

        EnemySpawner.Instance.SpawnStop();
        EnemySpawner.Instance.FindAllEnemiesDeSpawn();

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
        SceneManager.LoadScene(1);

        killenemys = 0;
        totalGold = 0;
        
    }
}
