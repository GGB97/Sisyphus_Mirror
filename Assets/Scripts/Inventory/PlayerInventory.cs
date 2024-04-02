using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : ItemGrid
{
    //public Dictionary<ItemType, List<InventoryItem>> playerInventory = new Dictionary<ItemType, List<InventoryItem>>();//플레이어가 장착한 아이템들

    private List<PanelSlot> addableSlotList = new List<PanelSlot>();//추가 가능한 블럭의 리스트
    private List<PanelSlot> clearSlotList = new List<PanelSlot>();//지워야 하는 + 블럭 리스트
    private List<PanelSlot> subtractSlotList = new List<PanelSlot>();//더이상 추가 불가능한 블럭의 리스트
    private List<Vector2Int> fourVector = new List<Vector2Int>() { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) };
    private List<InventoryItem> combineList = new List<InventoryItem>();

    // Start is called before the first frame update
    protected override void Start()
    {
        SetGridSize(10, 10);//그리드 설정
        base.Start();
        CreateBaseBlock(gridSizeWidth, gridSizeHeight);
        maxCount = 100;
    }

    public void CreateBaseBlock(int gridWidth, int gridHeight)//중앙에 block 추가
    {
        for (int x = gridWidth / 2 - 2; x <= gridWidth / 2; x++)//중앙 바꾸기
        {
            for (int y = gridHeight / 2 - 2; y <= gridHeight / 2; y++)
            {
                addableSlotList.Add(panelSlots[x, y]);//생성 시 리스트에 추가
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
    }
    public void ShowRandomAddableSlot()
    {
        //isSetting = true;// 설정을 한 번 한 것으로 설정
        currentAddSlot = 0;//현재 추가 슬롯
        for (int i = 0; i < 10; i++)//추가 되어야할 칸이 최소 5개
        {
            foreach (PanelSlot go in addableSlotList)
            {
                if (currentAddSlot >= minAddSlot)
                    break;

                ShowAddableSlot(go, ref currentAddSlot);
            }
            if (currentAddSlot >= minAddSlot)
                break;
        }
    }
    public void ShowAddableSlot(PanelSlot panelSlot, ref int currentAddSlot)
    {
        List<Vector2Int> addPositionArr = new List<Vector2Int>();//추가 가능한 칸의 좌표 배열
        int x;
        int y;
        foreach (var vector in fourVector)
        {
            x = panelSlot.posX + vector.x;
            y = panelSlot.posY + vector.y;
            if (GridPositionCheck(x, y) == true)//x,y가 Grid 안에 있는지 체크
            {
                if (panelSlots[x, y].CompareState(PanelSlotState.Null)|| panelSlots[x, y].CompareState(PanelSlotState.Add))//옆이 널일 때만 List에 넣음
                {
                    addPositionArr.Add(new Vector2Int(x, y));
                }
            }
        }
        if (addPositionArr.Count > 0) // 자신 주위에 null 또는 add 블럭인지
        {
            int rnd;
            foreach (var addPosition in addPositionArr)
            {
                if (panelSlots[addPosition.x, addPosition.y].CompareState(PanelSlotState.Add))//add 블럭이면 건너 뛴다.
                    continue;

                if (currentAddSlot >= minAddSlot)
                    break;

                rnd = Random.Range(0, 4);
                if (rnd == 0) //당첨되면
                {
                    x = addPosition.x;
                    y = addPosition.y;
                    clearSlotList.Add(panelSlots[x, y]);
                    panelSlots[x, y].ChangeSlotState(PanelSlotState.Add);
                    currentAddSlot++;
                    //Debug.Log("칸 추가 성공");
                }
            }
        }
        else
        {
            subtractSlotList.Add(panelSlot);
        }
    }
    public void SubtractToAddableSlotList()
    {
        if (subtractSlotList.Count > 0)
        {
            foreach (var slot in subtractSlotList)
            {
                addableSlotList.Remove(slot);
                Debug.Log($"[{slot.posX},{slot.posY}");
            }
            subtractSlotList.Clear();
        }
    }
    public void AddToAddableSlotList(PanelSlot panelSlot)
    {
        addableSlotList.Add(panelSlot);//집어넣고
        clearSlotList.Remove(panelSlot);//Clear에 있던 것을 제거
    }
    public void CreateAddSlot()
    {
        Vector2Int tileGridPosition = GetTileGridPosition(Input.mousePosition);//클릭했을 때의 마우스 좌표
        panelSlots[tileGridPosition.x, tileGridPosition.y].ChangeSlotState(PanelSlotState.Empty);//Add를 Empty로 변경
        AddToAddableSlotList(panelSlots[tileGridPosition.x, tileGridPosition.y]);//AddList에 추가.
        ClearToClearSlotList();//Clear 초기화
        SubtractToAddableSlotList();
        //foreach (var slot in addableSlotList)
        //{
        //    Debug.Log($"[{slot.posX},{slot.posY}");
        //}
        Debug.Log($"총 개수 : {addableSlotList.Count}");
        //isSetting = false;
        InventoryController.Instance.addCount -= 1;
        if (InventoryController.Instance.addCount > 0)
        {
            ShowRandomAddableSlot();
        }
    }
    public void ClearToClearSlotList()
    {
        foreach (var panelSlot in clearSlotList)
        {
            panelSlot.ChangeSlotState(PanelSlotState.Null);//안에 들어있던 것들 null로 변경
        }
        clearSlotList.Clear();//리스트 참조 초기화
    }

    public void AddBigInventory()
    {
        for (int x = 2; x < gridSizeWidth - 2; x++)
        {
            for (int y = 2; y < gridSizeHeight - 2; y++)
            {
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
    }
    public bool FindSameWeaponItem(int targetId)//무기 중에서 같은 id를 가지고 있는 것이 3개 이상일 경우 true 리턴
    {
        List<InventoryItem> weaponList = inventory[ItemType.Weapon];
        int count = 0;
        if (weaponList == null)
        {
            return false;
        }
        else
        {
            foreach (InventoryItem weapon in weaponList)
            {
                if (targetId == weapon.itemSO.Id)//아이디가 같은지 확인
                {
                    count++;
                    if (count >= 3)
                        break;
                    //리스트에 따로 저장해서 나중에 한번에 접근할 수 있게 하면 좋을듯
                }
            }
            if (count >= 3)//변수로 바꿔야 할 듯
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public void SetCombineList(int targetId)//무기 중에서 같은 id를 가지고 있는 것이 3개 이상일 경우 true 리턴
    {
        List<InventoryItem> weaponList = inventory[ItemType.Weapon];
        int count = 0;
        if (weaponList == null)
        {
            return;
        }
        else
        {
            foreach (InventoryItem weapon in weaponList)
            {
                if (targetId == weapon.itemSO.Id)//아이디가 같은지 확인
                {
                    count++;
                    combineList.Add(weapon);
                    if (count >= 2)
                        break;
                    //리스트에 따로 저장해서 나중에 한번에 접근할 수 있게 하면 좋을듯
                }
            }
        }
    }
    public void DeleteSameItem(InventoryItem currentItem)
    {
        int itemId = currentItem.itemSO.Id;
        DeleteItem(currentItem);//현재 아이템 삭제
        SetCombineList(itemId);//같은 id 아이템을 찾아서 List에 넣음

        foreach (var item in combineList)//List에서 하나씩 꺼내서 삭제
        {
            DeleteItem(item);
        }

        ResetCombineList();//리스트 초기화
    }
    public void DeleteItem(InventoryItem item)
    {
        PickUpItem(item.onGridPositionX, item.onGridPositionY);//아이템이 있던 자리 null로 변경
        SubtractItemFromInventory(item);//플레이어 인벤토리에서 빼기
        Destroy(item.gameObject);//게임 오브젝트 삭제 (화면에 시각적으로 보이는 아이템)
        AddCurrentCount(-1);//카운트 감소
    }
    public void ResetCombineList()
    {
        combineList.Clear();
    }
}
