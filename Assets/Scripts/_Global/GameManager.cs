using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    public bool isGameover = false;
    private int _playerID;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerPrefab;
    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = Instantiate(Resources.Load<Player>(DataBase.Player.Get(_playerID).prefabPath));
            }
            return _player;
        }
        private set { _player = value; }
    }

    public void Gameover()
    {
        DungeonManager.Instance.isStarted = false;

        EnemySpawner.Instance.SpawnStop();
        EnemySpawner.Instance.FindAllEnemiesDeSpawn();

        EditorApplication.isPaused = true;
    }

    public void SetPlayer(Player newPlayer)
    {
        Player = newPlayer;
        _playerID = Player.Data.id;
    }
}
