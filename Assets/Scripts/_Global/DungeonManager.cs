using Constants;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : SingletoneBase<DungeonManager>
{
    [SerializeField]
    private GameObject inventoryUI;//인벤토리 UI
    [SerializeField]
    private GameObject stageUI;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI stageText;

    public string inventoryTag = "inventoryUI";
    public string stageTag = "stageUI";
    public string stageTextName = "FloorText";
    public string TimeTextName = "TimeText";

    public float timeLimit = 50f;
    public float currentTime = 0f;
    public DungeonState gameState;
    public bool isStageCompleted = false;
    public int currnetstage = 0;


    public event Action OnStageStart;
    public event Action<int> OnStageClear;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        OnStageStart += ResetPlayerPos;
    }

    public override void Init()
    {
        inventoryUI = GameObject.FindGameObjectWithTag(inventoryTag);
        stageUI = GameObject.FindGameObjectWithTag(stageTag);
        //InventoryController.Instance.nextStage += CloseInventory;

        if (inventoryUI == null && stageUI == null)
        {
            Debug.Log("찾기 실패");
        }
        else
        {
            TextMeshProUGUI[] arr = stageUI.GetComponentsInChildren<TextMeshProUGUI>();
            stageText = System.Array.Find(arr, x => x.name == stageTextName);
            timeText = System.Array.Find(arr, x => x.name == TimeTextName);

            //Debug.Log("찾기 성공");
            //inventoryUI.SetActive(false);
        }

        if (InventoryController.Instance != null)
            InventoryController.Instance.nextStage += CloseInventory;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }

    private void Update()
    {
        if (gameState == DungeonState.Playing)
        {
            UpdateTimeText();

            if (isStageCompleted)
            {
                StageClear();
            }

            if (currentTime > 0.0f)
            {
                currentTime -= Time.deltaTime;//시간 빼기
                currentTime = Mathf.Clamp(currentTime, 0.00f, timeLimit);
            }
            else if (currentTime <= 0 && EnemySpawner.Instance.arriveBoss == true)
            {
                // 끝
                GameManager.Instance.Gameover();
            }
            else
            {
                //모든 몬스터 죽기
                StageClear();
            }
        }
    }
    public void UpdateTimeText()
    {
        timeText.text = currentTime.ToString("N2");
    }
    public void SetStageAndStart()//스테이지 설정하고 시작 매개변수로 스테이지 정보 받아오기? 아니면 매개변수 없이 메서드 내에서 랜덤한 값으로 맵을 리소시스에 있는 프리팹으로 받아오기
    {
        //맵정보 받아오면 적용
        currnetstage += 1;
        isStageCompleted = false;

        if (currnetstage % 5 == 0)//5스테이지 마다 시간 다르게 적용?
        {
            timeLimit = StageTimeLimit.Boss;//나중에 상수로 따로 빼두면 좋음
        }
        else
        {
            timeLimit = StageTimeLimit.Normal;//나중에 상수로 따로 빼두면 좋음
        }

        currentTime = timeLimit;//시간 설정
        stageText.text = String.Format("Stage : " + currnetstage.ToString());

        gameState = DungeonState.Playing;
        OnStageStart?.Invoke();
        EnemySpawner.Instance.GameStart();
    }
    public void StageClear()//스테이지 끝나면 호출
    {
        gameState = DungeonState.Clear;

        //모든 동작 멈추고
        EnemySpawner.Instance.SpawnStop();
        EnemySpawner.Instance.AllEnemiesDeSpawn();

        // 기본 1개 + 10스테이지마다 하나씩 늘어나게?
        GameManager.Instance.Player.GetComponent<Player>().ChangeRune(1 + (currnetstage / 10));

        OnStageClear?.Invoke(currnetstage);
        QuestManager.Instance.NotifyQuest(QuestType.StageClear, 10, 1);//스테이지 클리어 시 카운트 증가

        Invoke(nameof(InvokeInventory), 2f);
        //Invoke("OpenInventory", 1f);//인벤토리 열기
    }

    void InvokeInventory()
    {
        UIManager.Instance.FadeOut(0.5f, () =>
        {
            OpenInventory();
            UIManager.Instance.FadeIn(0.5f);
        });
    }

    public void OpenInventory()
    {
        //위 혹은 여기에 플레이어 동작 , 몬스터 소환 멈추는 코드

        inventoryUI.SetActive(true);
        InventoryController.Instance.AddBlock();
        InventoryStats.Instance.UpdateStatsPanel();
        //InventoryController.Instance.OnStoreReroll();
    }
    public void CloseInventory()
    {
        //인벤토리에 장착한 아이템 적용하기 있다면 패스
        inventoryUI.SetActive(false);
        SetStageAndStart();//스테이지 생성 시작
        //플레이어 위치 조정
        //맵을 동적으로 구워야 하면 적용
    }

    void ResetPlayerPos()
    {
        GameManager.Instance.Player.transform.position = Vector3.zero;
    }

    public void Print()
    {
        Debug.Log("게임 매니저 생성");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
