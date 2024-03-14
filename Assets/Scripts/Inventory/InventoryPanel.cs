using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{ 
    [SerializeField] GameObject panelSlotPrefab; //판텔 슬롯의 프리팹
    ItemGrid parentGrid; //부모 Grid

    public PanelSlot[,] panelSlots; // 패널의 슬롯 배열
    private List<PanelSlot> addableSlotList = new List<PanelSlot>();
    private List<PanelSlot> clearSlotList = new List<PanelSlot>();
    private List<Vector2Int> fourVector = new List<Vector2Int>() { new Vector2Int(-1, 0) , new Vector2Int(1,0),new Vector2Int(0,-1),new Vector2Int(0,1) };
    int AddCount = 2;//추가 칸 개수
    int minAddSlot = 5;
    int currentAddSlot = 0;

    private void Start()
    {
    }
    public void Init(int width,int height)
    {
        panelSlots = new PanelSlot[width,height];
        parentGrid = GetComponentInParent<ItemGrid>();
        if (parentGrid != null)
        {
            for (int x = 0; x < width; x++) //아이템 
            {
                for (int y = 0; y < height; y++)
                {
                    PanelSlot go = Instantiate(panelSlotPrefab).GetComponent<PanelSlot>();//한 칸 생성
                    go.transform.SetParent(transform);
                    panelSlots[x, y] = go; //해당하는 칸에 넣음
                    panelSlots[x, y].ChangeSlotState(PanelSlotState.Null);
                    panelSlots[x, y].SetPosition(x, y);
                }
            }
            for (int x = width / 2 - 1; x <= width / 2; x++)
            {
                for (int y = height / 2 - 1; y <= height / 2; y++)
                {
                    addableSlotList.Add(panelSlots[x, y]);//생성 시 리스트에 추가
                    panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
                }
            }

            currentAddSlot = 0;//현재 추가 슬롯
            while (currentAddSlot < minAddSlot)//추가 되어야할 칸이 최소 5개
            {
                foreach (PanelSlot go in addableSlotList)
                {
                    if (currentAddSlot >= minAddSlot)
                        break;

                    AddSlot(go, go.posX, go.posY, ref currentAddSlot);
                }
            }

            //while (AddCount > 0)//추가할 칸 개수가 양수일 때만 
            //{
            //currentAddSlot = 0;//현재 추가 슬롯
            //while (currentAddSlot <= minAddSlot)//추가 되어야할 칸이 최소 5개
            //{
            //    foreach (PanelSlot go in addableSlotList)
            //    {

            //    }
            //}
            //}
        }
    }
    public void AddSlot(PanelSlot panelSlot ,int posX, int posY,ref int currentAddSlot)
    {
        List<Vector2Int> addPositionArr = new List<Vector2Int> ();//추가 가능한 칸의 좌표 배열
        int x;
        int y;

        foreach(var vector in fourVector) 
        {
            x = posX + vector.x;
            y = posY + vector.y;
            if (panelSlots[x, y].CompareState(PanelSlotState.Null))
            {
                addPositionArr.Add(new Vector2Int(x,y));
            }
        }
        if (addPositionArr.Count > 0) // 칸 변경
        {
            int rnd;
            foreach (var addPosition in addPositionArr)
            {
                if (currentAddSlot >= minAddSlot)
                    break;

                rnd = Random.Range(0, 2);
                if (rnd == 0) //당첨되면
                {
                    x = addPosition.x;
                    y = addPosition.y;
                    clearSlotList.Add(panelSlots[x, y]);
                    panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
                    currentAddSlot++;
                    Debug.Log("칸 추가 성공");
                }
            }
        }
    }
    public void AllClearSlotList()
    {
        
    }
}
