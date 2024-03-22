using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputSettings;

public class ItemGrid : MonoBehaviour
{
    #region grid
    public const float TileSizeWidth = 64f;//가로 타일의 사이즈
    public const float TileSizeHeight = 64f;//세로 타일의 사이즈

    public Dictionary<ItemType, List<InventoryItem>> inventory = new Dictionary<ItemType, List<InventoryItem>>();//인벤토리에 들어있는 아이템들
    public int maxCount = 1;
    public int currentCount = 0;
    //public int currnetCount
    //{
    //    get
    //    {
    //        int num = 0;
    //        foreach (var itemType in inventory)
    //        {
    //            foreach (var item in itemType.Value)
    //            {
    //                if (item != null)
    //                {
    //                    Debug.Log($"{itemType.Key} - {item.itemData.itemIcon.name}");
    //                    num++;
    //                }
    //            }
    //        }
    //        return num;
    //    }
    //}

    public InventoryItem[,] inventoryItemSlot;//해당칸의 아이템 정보를 담는 배열

    public InventoryPanel inventoryPanel; // 한 칸들의 모음 판넬

    RectTransform rectTransform;//UI 그리드의 트랜스 폼

    protected int gridSizeWidth = 10;//가로 길이
    protected int gridSizeHeight = 10; // 세로 길이

    protected Vector2 mousePositionOnTheGrid; //그리드 왼쪽 상단에서 마우스의 위치 값
    protected Vector2Int tileGridPosition = new Vector2Int(); //그리드 위에서의 좌표
    #endregion

    #region panel
    //[SerializeField] GameObject panelPrefab;
    [SerializeField] GameObject panelSlotPrefab; //판넬 슬롯의 프리팹
    public PanelSlot[,] panelSlots; //패널의 슬롯 바닥 배열

    //public bool isSetting = false;
    protected int minAddSlot = 5;
    protected int currentAddSlot = 0;
    #endregion

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    protected virtual void Start()
    {
        Init(gridSizeWidth, gridSizeHeight);// 가로, 세로 길이만큼 grid 칸 생성
    }

    private void Init(int width, int height)//그리드 초기 생성
    {
        inventoryPanel = GetComponentInChildren<InventoryPanel>();
        GridInit(width, height);
        PanelInit(width, height);
    }
    public void AddItemToInventory(InventoryItem seletecteditem)//플레이어 인벤토리만 이 메서드를 사용한다. 아이템 딕셔너리에 넣는 기능
    {
        List<InventoryItem> itemList = null;
        if (inventory.ContainsKey(seletecteditem.itemData.itemType))//키가 존재하다면
        {
            itemList = inventory[seletecteditem.itemData.itemType];
            itemList.Add(seletecteditem);
            inventory[seletecteditem.itemData.itemType] = itemList;
        }
        else//키가 존재하지 않다면
        {
            itemList = new List<InventoryItem>() { seletecteditem };
            inventory.Add(seletecteditem.itemData.itemType, itemList);
        }
        //ItemManager.Instance.OnEquip(seletecteditem.itemData.id, seletecteditem.itemData.itemType);
        Debug.Log($"아이템 추가 - {seletecteditem.itemData.itemIcon.name}");
    }
    public void SubtractItemFromInventory(InventoryItem seletecteditem)//플레이어 인벤토리만 이 메서드를 사용한다. 아이템 딕셔너리에서 빼는 기능
    {
        if (inventory.ContainsKey(seletecteditem.itemData.itemType))//키가 존재하다면
        {
            inventory[seletecteditem.itemData.itemType].Remove(seletecteditem);
            Debug.Log($"아이템 빼기 - {seletecteditem.itemData.itemIcon.name}");
            //ItemManager.Instance.OnUnequip(seletecteditem.itemData.id, seletecteditem.itemData.itemType);
        }
        else//키가 존재하지 않다면
        {
            Debug.Log($"아이템이 존재하지 않습니다.");
        }
    }
    public void AddCurrentCount(int num)//현재 Grid에 있는 아이템의 수를 num 만큼 증가시킨다.
    {
        currentCount += num;
    }
    private void GridInit(int width, int height)//Gird 공간 마련
    {
        inventoryItemSlot = new InventoryItem[width, height];//공간 마련
        Vector2 size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
        rectTransform.sizeDelta = size;
    }
    private void PanelInit(int width, int height)//바닥 공간 마련
    {
        panelSlots = new PanelSlot[width, height];

        if (inventoryPanel != null) //판넬이 있을 때
        {
            for (int y = 0; y < height; y++) //아이템 
            {
                for (int x = 0; x < width; x++)
                {
                    PanelSlot go = Instantiate(panelSlotPrefab).GetComponent<PanelSlot>();//한 칸 생성
                    go.transform.SetParent(inventoryPanel.transform);
                    panelSlots[x, y] = go; //해당하는 칸에 넣음
                    //panelSlots[x, y].ChangeSlotState(PanelSlotState.Null);
                    panelSlots[x, y].SetPosition(x, y);//제거 고려
                    //parentGrid.inventoryItemSlot[x,y].panelSlot = panelSlots[x, y];
                }
            }
            
        }
    }
    public void SetGridSize(int width, int height) //그리드의 사이즈를 설정
    {
        gridSizeWidth = width;
        gridSizeHeight = height;
    }
    public Vector2Int GetTileGridPosition(Vector2 mousePosition) //그리드 판의 왼쪽 상단을 0,0 오른쪽 하단을 max,max로하는 그리드 위에서의 좌표반환
    {
        mousePositionOnTheGrid.x = mousePosition.x - rectTransform.position.x;//Grid 와 마우스의 떨어짐 정도
        mousePositionOnTheGrid.y = rectTransform.position.y - mousePosition.y;//Grid 와 마우스의 떨어짐 정도

        tileGridPosition.x = (int)(mousePositionOnTheGrid.x / TileSizeWidth);//Grid 상 좌표로 변환
        tileGridPosition.y = (int)(mousePositionOnTheGrid.y / TileSizeHeight);//Grid 상 좌표로 변환

        return tileGridPosition;
    }
    //public Vector2 GridToScreenPosition(Vector2Int gridPosition)
    //{
    //    Vector2 screenPosition = new Vector2();
    //    screenPosition.x = rectTransform.position.x + gridPosition.x * TileSizeWidth + TileSizeWidth / 2;
    //    screenPosition.y = rectTransform.position.y - gridPosition.y * TileSizeHeight + TileSizeHeight / 2;
    //    return screenPosition;
    //}
    public bool CheckMaxCount()//현재 Grid에 들어있는 아이템의 카운트를 체크
    {
        if (maxCount > currentCount)
            return false;
        else
            return true;
    }
    public bool PlaceItem(InventoryItem inventoryItem,int posX,int posY, ref InventoryItem overlapitem) //그리드 좌표 x,y에 아이템 배치
    {
        if (BoundryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false) //아이템이 Grid 안에 있는지 체크 
        {
            return false;
        }

        //if (OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapitem) == false)//물체 마지막 하단 칸에 설치할 수 있는지 체크
        //{
        //    overlapitem = null; //겹치는 것이 있으면 overlap 초기화
        //    return false; //실패 반환
        //}

        //if (overlapitem != null) //겹치는 것이 있다면
        //{
        //    CleanGridReference(overlapitem);//겹치는 곳 null로 만듬
        //}
        if (CheckMaxCount() == true)//최대를 넘겼는지 확인
        {
            return false;
        }
        
        PlaceItem(inventoryItem, posX, posY);//새로운 아이템 놓기

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)//좌표에 아이템 설치
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform); //현재 그리드를 자신의 부모로 설정

        for (int x = 0; x < inventoryItem.WIDTH; x++) // 아이템 배열에 아이템 크기에 해당하는 칸 수만큼 저장.
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
                panelSlots[posX + x, posY + y].ChangeSlotState(PanelSlotState.Full);//바닥 교체
            }
        }

        inventoryItem.onGridPositionX = posX; //자신의 첫 칸 정보인 posX를 저장
        inventoryItem.onGridPositionY = posY; //자신의 첫 칸 정보인 posY를 저장

        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);//물체의 중심 설정

        rectTransform.localPosition = position; //지역 위치를 position값으로
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY) //Grid 상에서 물체 중심의 local 위치
    {
        Vector2 position = new Vector2();
        position.x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.WIDTH / 2;//자신의 중심을 표현
        position.y = -(posY * TileSizeHeight + TileSizeHeight * inventoryItem.HEIGHT / 2);//자신의 중심을 표현
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapitem)//놓으려는 공간의 아이템이 하나의 아이템이라면 true 아니면 false
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)//물체가 있다면
                {
                    if (overlapitem == null)//이전에 겹치 것이 없다면
                    {
                        overlapitem = inventoryItemSlot[posX + x, posY + y];//해당 칸에 겹치는 오브젝트 참조
                    }
                    else
                    {
                        if (overlapitem != inventoryItemSlot[posX + x, posY + y]) //같은 아이템이 아니라면
                        { 
                            return false; //아니면 false
                        }
                    }
                }
            }
        }

        return true; 
    }
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)//인벤토리 공간에 아이템을 설치할 수 있는지 체크 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null || !panelSlots[posX + x, posY + y].CompareState(PanelSlotState.Empty)) //물체의 크기만큼 바닥과 설치할 공간이 있는지 확인
                {    
                    return false; //아니면 false
                }
            }
        }

        return true;
    }

    public InventoryItem PickUpItem(int x, int y)//x,y에 해당하는 아이템을 반환 있던 자리는 null
    {
        InventoryItem toReturn = inventoryItemSlot[x, y]; //아이템 임시 저장.

        if (toReturn == null) { return null; } //빈 공간을 터치했을 때 널 리턴

        CleanGridReference(toReturn);//있던 자리 null 처리

        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)//아이템의 정보로 차지하는 공간 만큼 초기화
    {
        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;//아이템 공간만큼 null
                panelSlots[item.onGridPositionX + ix, item.onGridPositionY + iy].ChangeSlotState(PanelSlotState.Empty);//바닥 Empty 변경
            }
        }
    }

    bool PositionCheck(int posX, int posY) //Grid 안에 있고 바닥이 비었는지 체크 후 bool 값 리턴
    {
        if (GridPositionCheck(posX, posY) == false)
        {    
            return false;
        }

        if (!panelSlots[posX, posY].CompareState(PanelSlotState.Empty))//비어있는지 체크
        {
            return false;
        }
        return true;
    }
    public bool GridPositionCheck(int posX, int posY)//Grid 안에 있는지 체크 
    {
        if (posX < 0 || posY < 0)
        {
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }


    public bool BoundryCheck(int posX, int posY, int width, int height)//Grid 안에 아이템이 포함될 수 있는지 체크
    {
        for (int x = posX; x < posX + width; x++)
        {
            for (int y = posY; y < posY + height; y++)
            {
                if (PositionCheck(x, y) == false) //해당 칸에 놓을 수 없다면
                {
                    return false;
                }
            }
        }
        //if (PositionCheck(posX, posY) == false) //물체의 첫 칸의 위치가 grid안에 없다면
        //{
        //    return false;
        //}

        //posX += width - 1;
        //posY += height - 1;

        //if (PositionCheck(posX, posY) == false) //물체의 우하단 위치가 grid안에 없다면
        //{
        //    return false;
        //}

        return true;//놓을 수 있다면 
    }

    public InventoryItem GetItem(int x, int y)//해당 칸의 아이템 정보
    {
        return inventoryItemSlot[x, y];
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)//전체 배열에서 아이템을 설치할 수 있는지 체크
    {
        int height = gridSizeHeight - itemToInsert.HEIGHT +1;
        int width = gridSizeWidth - itemToInsert.WIDTH +1;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                if (CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT) == true)//x, y에 아이템을 설치할 수 있는지 체크 후 true
                {
                    return new Vector2Int(x, y);//설치할 수 있으면 x,y 값 리턴
                }
            }
        }

        return null;//설치 불가능하면 null리턴
    }
}
