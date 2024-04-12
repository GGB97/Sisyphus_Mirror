using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    [SerializeField] private int _playerID = 20000002;
    [SerializeField] private Player _player;

    public GameOverUI GameOverUI;

    public int killenemys;
    public int totalGold;

    public Action onGameOverEvent;

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

    public void Gameover()
    {
        DungeonManager.Instance.gameState = GameState.Fail;

        EnemySpawner.Instance.SpawnStop();
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
        SceneManager.sceneLoaded -= DungeonManager.Instance.OnSceneLoaded;
        Destroy(DungeonManager.Instance.gameObject);
        Destroy(Player.gameObject);
        SceneManager.LoadScene(1);
        this.Player.playerReset();

        killenemys = 0;
        totalGold = 0;
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= DungeonManager.Instance.OnSceneLoaded;
    }
}
