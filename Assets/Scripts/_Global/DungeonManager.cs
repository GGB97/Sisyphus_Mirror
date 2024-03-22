using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public bool isStarted = false;
    public int currnetstage = 0;

    private void Start()
    {
    }
    private void Awake()
    {
        inventoryUI = GameObject.FindGameObjectWithTag(inventoryTag);
        stageUI = GameObject.FindGameObjectWithTag(stageTag);
        InventoryController.Instance.nextStage += CloseInventory;

        if (inventoryUI == null && stageUI == null)
        {
            Debug.Log("찾기 실패");
        }
        else
        {
            TextMeshProUGUI[] arr = stageUI.GetComponentsInChildren<TextMeshProUGUI>();
            stageText = System.Array.Find(arr, x => x.name == stageTextName);
            timeText = System.Array.Find(arr, x => x.name == TimeTextName);

            Debug.Log("찾기 성공");
            inventoryUI.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (isStarted == true)
        {
            UpdateTimeText();

            if (currentTime > 0.0f)
            {
                currentTime -= Time.deltaTime;//시간 빼기
                currentTime = Mathf.Clamp(currentTime, 0.00f, timeLimit);
            }
            else
            {
                //모든 몬스터 죽기
                EndStage();
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
        if (currnetstage % 5 == 0)//5스테이지 마다 시간 다르게 적용?
        {
            timeLimit = 60f;//나중에 상수로 따로 빼두면 좋음
        }
        else
        {
            timeLimit = 5f;//나중에 상수로 따로 빼두면 좋음
        }

        currentTime = timeLimit;//시간 설정
        stageText.text = String.Format("Stage : "+ currnetstage.ToString());

        isStarted = true; 
    }
    public void EndStage()//스테이지 끝나면 호출
    {
        isStarted = false;
        //모든 동작 멈추고
        Invoke("OpenInventory",1f);//인벤토리 열기
    }
    public void OpenInventory()
    {
        //위 혹은 여기에 플레이어 동작 , 몬스터 소환 멈추는 코드
        inventoryUI.SetActive(true);
    }
    public void CloseInventory()
    {
        //인벤토리에 장착한 아이템 적용하기 있다면 패스
        inventoryUI.SetActive(false);
        SetStageAndStart();//스테이지 생성 시작
        //플레이어 위치 조정
        //맵을 동적으로 구워야 하면 적용
    }
    public override void Init()
    {
        
    }
    public void Print()
    {
        Debug.Log("게임 매니저 생성");
    }
}