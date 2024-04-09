using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : ItemGrid
{
    //public Dictionary<ItemType, List<InventoryItem>> playerInventory = new Dictionary<ItemType, List<InventoryItem>>();//플레이어가 장착한 아이템들

    private List<PanelSlot> addableSlotList = new List<PanelSlot>();//추가 가능한 블럭의 리스트
    private List<PanelSlot> clearSlotList = new List<PanelSlot>();//지워야 하는 + 블럭 리스트
    private List<PanelSlot> subtractSlotList = new List<PanelSlot>();//더이상 추가 불가능한 블럭의 리스트
    private List<Vector2Int> fourVector = new List<Vector2Int>() { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) };
    private List<InventoryItem> combineList = new List<InventoryItem>();
    private Vector2Int blockStartPosition = new Vector2Int(4, 4);

    // Start is called before the first frame update
    protected override void Start()
    {
        SetGridSize(10, 10);//그리드 설정
        base.Start();
        //CreateBaseBlock(gridSizeWidth, gridSizeHeight);
        maxCount = 100;
        CreateRandomBaseBlock();
    }

    public int CreateBaseBlock(int gridWidth, int gridHeight)//중앙에 block 추가
    {
        int count = 0;
        for (int x = gridWidth / 2 - 1; x <= gridWidth / 2; x++)//중앙 바꾸기
        {
            for (int y = gridHeight / 2 - 1; y <= gridHeight / 2; y++)
            {
                addableSlotList.Add(panelSlots[x, y]);//생성 시 리스트에 추가
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
                count++;
            }
        }
        return count;
    }
    public void CreateRandomBaseBlock(int itemId = 10113011)//중앙에 block 추가 
    {
        int baseCount = CreateBaseBlock(gridSizeWidth, gridSizeHeight);
        int count = GameManager.Instance.Player.Data.startInventory;// 칸 수 설정
        count -= baseCount;

        if (count <= 0)
            return;

        ItemSO item = DataBase.Weapon.Get(itemId);//id 에 해당하는 무기 정보
        int itemWidth = item.IconWidth; //가로
        int itemHeight = item.IconHeight;//세로

        for (int x = 0; x < itemWidth; x++)
        {
            for (int y = 0; y < itemHeight; y++)
            {
                if (panelSlots[blockStartPosition.x + x, blockStartPosition.y + y].CompareState(PanelSlotState.Null) == true)
                {
                    addableSlotList.Add(panelSlots[blockStartPosition.x + x, blockStartPosition.y + y]);//생성 시 리스트에 추가
                    panelSlots[blockStartPosition.x + x, blockStartPosition.y + y].ChangeSlotState(PanelSlotState.Empty);
                    count--;
                }
            }
        }
        if (count > 0)
        {
            //랜덤 칸 증가
            for (int i = 0; i < count; i++)
            { 
                RandomAddbleSlot();//칸 추가
            }
        }
        ItemManager.Instance.init();
        //스타트 아이템 배치
        //룬 스톤 배치
    }
    public void RandomAddbleSlot()//랜덤 칸 확장
    {
        int rnd;
        int listCount = 0;
        int x;
        int y;

        foreach (PanelSlot go in addableSlotList)
        {
            listCount += CheckFourPosition(go);//clear에 넣음
        }

        if (listCount > 0)//추가할 요소가 있을 때만
        {
            rnd = Random.Range(0, listCount);//리스트 요소(좌표)를 하나 선택
            x = clearSlotList[rnd].posX;
            y = clearSlotList[rnd].posY;

            addableSlotList.Add(panelSlots[x, y]);//생성 시 리스트에 추가
            panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);//해당 칸 Empty로 변경
            clearSlotList.RemoveAt(rnd);//해당 인덱스 정보 삭제 후
            foreach (PanelSlot go in clearSlotList)
            {
                go.ChangeSlotState(PanelSlotState.Null);
            }
            ClearToClearSlotList();//Clear 초기화 
        }
    }
    public void ShowRandomAddableSlot()
    {
        int addCount = InventoryController.Instance.addCount;
        InventoryController.Instance.addBlockDescription.SetText(addCount);
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
    public int CheckFourPosition(PanelSlot panelSlot)
    {
        int x;
        int y;
        int count = 0;
        foreach (var vector in fourVector)
        {
            x = panelSlot.posX + vector.x;
            y = panelSlot.posY + vector.y;
            if (GridPositionCheck(x, y) == true)//x,y가 Grid 안에 있는지 체크
            {
                if (panelSlots[x, y].CompareState(PanelSlotState.Null))//옆이 Null일 때만 List에 넣음
                {
                    panelSlots[x, y].ChangeSlotState(PanelSlotState.Add);
                    clearSlotList.Add(panelSlots[x, y]);
                    count++;
                }
            }
        }
        return count;
    }
    public void newAddableList()
    {
        List<Vector2Int> addPositionArr = new List<Vector2Int>();//추가 가능한 칸의 좌표 배열
        int x;
        int y;
        foreach (var slot in addableSlotList)
        {
            foreach (var vector in fourVector)
            {
                x = slot.posX + vector.x;
                y = slot.posY + vector.y;
                if (GridPositionCheck(x, y) == true)//x,y가 Grid 안에 있는지 체크
                {
                    if (panelSlots[x, y].CompareState(PanelSlotState.Null) || panelSlots[x, y].CompareState(PanelSlotState.Add))//옆이 Null과 Add일 때만 List에 넣음
                    {
                        addPositionArr.Add(new Vector2Int(x,y));
                    }
                }
            }

            if (addPositionArr.Count == 0)
                subtractSlotList.Add(slot);

            addPositionArr.Clear();
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
                if (panelSlots[x, y].CompareState(PanelSlotState.Null)|| panelSlots[x, y].CompareState(PanelSlotState.Add))//옆이 Null과 Add일 때만 List에 넣음
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
            //subtractSlotList.Add(panelSlot);
        }
    }
    public void SubtractToAddableSlotList()
    {
        if (subtractSlotList.Count > 0)
        {
            foreach (var slot in subtractSlotList)
            {
                addableSlotList.Remove(slot);
                //Debug.Log($"뺀 칸의 정보 : {slot.posX},{slot.posY}");
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
        AddToAddableSlotList(panelSlots[tileGridPosition.x, tileGridPosition.y]);//AddList에 추가 및 clear에서 삭제
        newAddableList();//추카할 수 있는 칸들의 리스트를 최신화
        ClearToClearSlotList();//Clear 초기화
        SubtractToAddableSlotList();//더이상 추가 못하는 칸은 빼버린다.
        //foreach (var slot in addableSlotList)
        //{
        //    Debug.Log($"[{slot.posX},{slot.posY}");
        //}
        Debug.Log($"추가 가능 총 개수 : {addableSlotList.Count}");
        //foreach (var slot in addableSlotList)
        //{
        //    Debug.Log($"가능 칸 정보 : {slot.posX},{slot.posY}");
        //}
        //isSetting = false;
        InventoryController.Instance.addCount -= 1;
        if (InventoryController.Instance.addCount > 0 && addableSlotList.Count > 0 )
        {
            ShowRandomAddableSlot();
        }
        else
        {
            InventoryController.Instance.addBlockDescription.Active(false);
            InventoryController.Instance.isAdding = false;
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
