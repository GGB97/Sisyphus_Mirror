using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    public bool isGameover = false;
    [SerializeField] private int _playerID;
    [SerializeField] private Player _player;

    public GameObject gameoverUI;

    [SerializeField] private TextMeshPro floorText;
    [SerializeField] private TextMeshPro levelText;
    [SerializeField] private TextMeshPro killText;
    [SerializeField] private TextMeshPro goldText;

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

    public void Gameover()
    {
       // if (Player.isDie)
       // {
            DungeonManager.Instance.isStarted = false;

            EnemySpawner.Instance.SpawnStop();
            EnemySpawner.Instance.FindAllEnemiesDeSpawn();

            EditorApplication.isPaused = true;
            gameoverUI.SetActive(true);

            currentFloor = DungeonManager.Instance.currnetstage;
            currentLevel = Player.Data.LV;

            floorText.text = currentFloor.ToString();
            levelText.text = currentLevel.ToString();
            killText.text = killenemys.ToString();
            goldText.text = totalGold.ToString();
       // }
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
    }
}
