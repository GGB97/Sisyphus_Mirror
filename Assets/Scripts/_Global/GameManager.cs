using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletoneBase<GameManager>
{
    public float timeLimit = 50f;
    public float currentTime = 0f;
    public bool isStarted = false;
    public int currnetstage = 1;

    private void Start()
    {
        Instance.Print();
    }
    private void OnEnable()
    {
        Debug.Log("onEnable");
    }
    private void Update()
    {
        if (isStarted == true)
        {
            if (currentTime < timeLimit)
            {
                currentTime += Time.deltaTime;//시간 추가
            }
            else
            {
                
            }
        }
    }
    public override void Init()
    {
        timeLimit = 50f;
        currentTime = 0f;
        isStarted = false;
        currnetstage = 1;
    }
    public static void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
    public void Print()
    {
        Debug.Log("게임 매니저 생성");
    }
}
