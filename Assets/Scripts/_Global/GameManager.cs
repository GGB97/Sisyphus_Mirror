using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    public bool isGameover = false;


    public void Gameover()
    {
        DungeonManager.Instance.isStarted = false;

        EnemySpawner.Instance.SpawnStop();
        EnemySpawner.Instance.FindAllEnemiesDeSpawn();

        EditorApplication.isPaused = true;
    }
}
