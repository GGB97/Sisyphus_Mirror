using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    [SerializeField] private int _playerID = 20000002;
    [SerializeField] private Player _player;

    public GameOverUI GameOverUI;

    public int killenemys;
    public int totalGold;

    public Action onGamoverEvent;

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

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log(scene.name);

        //if (GameOverUI == null)
        //{
        //    GameOverUI = GameObject.FindObjectOfType<GameOverUI>();
        //}
    } 

    public void Gameover()
    {
        DungeonManager.Instance.gameState = GameState.Fail;

        EnemySpawner.Instance.SpawnStop();
        onGamoverEvent?.Invoke();

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
        SceneManager.LoadScene(1);

        killenemys = 0;
        totalGold = 0;
        
    }
}
