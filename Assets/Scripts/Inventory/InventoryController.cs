using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    public event Action nextStage;
    private static InventoryController instance;
    public static InventoryController Instance { get { return instance; } private set{ instance = value; } }
    //Dictionary< 아이템 종류() , List<id>> 
    //[HideInInspector]
    [SerializeField]
    private ItemGrid selectedItemGrid; //현재 그리드 정보
    public ItemGrid SelectedItemGrid { 
        get => selectedItemGrid; 
        set { 
            selectedItemGrid = value;
            //inventoryHighlight.SetParent(selectedItemGrid);
        } 
    }
    [SerializeField]
    private ItemGrid previousItemGird;//이전 그리드 정보

    public InventoryItem selectedItem;//현재 선택된 아이템
    InventoryItem overlapitem;
    RectTransform rectTransform;//선택된 아이템의 트랜스폼
    public GameObject itemDescriptionUI;//아이템 설명 UI 창

    //[SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab; //아이템 프리팹
    public Transform canvasTransform;

    public PlayerInventory playerInventoryGrid;//인벤토리 그리드
    public Storage storageGrid;//창고 그리드
    public StoreGrid storeGrid;     // 상점 그리드

    InventoryHighlight inventoryHighlight;

    public Sprite[] slotSprites; //슬롯의 스프라이트 배열
    public BlockColor[] blockColors;//등급의 색깔 배열
    public Dictionary<ItemGrade, BlockColor> BlockColorDictionary = new Dictionary<ItemGrade, BlockColor>(); 
    public int addCount = 6;//추가 칸 개수

    public Vector2 startPosition;//처음 위치
    public float startRotation;

    [SerializeField] public TextMeshProUGUI[] itemCost = new TextMeshProUGUI[5];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        inventoryHighlight = GetComponent<InventoryHighlight>();
        BlockColorDictionaryInit();
    }
    private void Start()
    {
        //SelectedItemGrid = itemGrid;
        //SelectedItemGrid.ShowRandomAddableSlot();
    }
    private void Update()
    {
        ItemIconDrag();

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    //InsertRandomItem();
        //}
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PrintAllPlayerInventory();
        }
        //if (selectedItemGrid == null) // 그리드 위에 없다면
        //{
        //    inventoryHighlight.Show(false); //하이라이트 끔
        //    return;
        //}
        //HandleHightlight();//하이트 

        //if (Input.GetMouseButtonDown(0))
        //{
        //    LeftMouseButtonPress();
        //}
    }
    public void BlockColorDictionaryInit()
    {
        foreach (var blockColor in blockColors)
        {
            BlockColorDictionary[blockColor.Grade] = blockColor;
        }
    }
    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    public void StartButton()//칸 확장 기능
    {
        addCount = 6;
        SelectedItemGrid = playerInventoryGrid;
        playerInventoryGrid.ShowRandomAddableSlot();
    }
    private void InsertRandomItem()//아이템 랜덤 생성 후 배치
    {
        if (selectedItemGrid != playerInventoryGrid) { return; }

        CreateRandomItem();//아이템 생성
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)//인벤토리에 아이템 배치
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null)//설치 불가능 이면 return
        {
            Destroy(itemToInsert.gameObject);
            Debug.Log("삭제완료");
            return;
        }

        //selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
        playerInventoryGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);

        if (selectedItemGrid == playerInventoryGrid) //메서드화 필요 //플레이어 인벤토리에 데이터 저장하기
        {
            playerInventoryGrid.AddItemToInventory(itemToInsert);//아이템 추가.
            selectedItemGrid.AddCurrentCount(1);
        }
    }
    InventoryItem itemToHighlight;

    private void HandleHightlight()//하이라이트 표시하기
    {
        Vector2Int positionOnGrid = GetTileGridPosition();//Grid 상 마우스 위치 좌표
        if (selectedItem == null) //선택한 것이 아무것도 없으면
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x , positionOnGrid.y);// x, y에 해당하는 아이템 정보를 가져옴
            if (itemToHighlight != null) //마우스 위치에 객체가 있다면
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else//마우스의 위치에 아무것도 없다면
            {
                inventoryHighlight.Show(false);//하이라이트 끔
            }
        }
        else //선택한 것이 있다면
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, selectedItem.WIDTH, selectedItem.HEIGHT));//활성화
            inventoryHighlight.SetSize(selectedItem);//사이즈 지정
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem,positionOnGrid.x,positionOnGrid.y); //위치 지정
        }

    }

    private void CreateRandomItem() //아이템 랜덤 생성
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>(); //빈 아이템 객체 생성
        selectedItem = inventoryItem;//선택한 아이템 설정

        rectTransform = inventoryItem.GetComponent<RectTransform>();//트랜스폼 가져옴
        rectTransform.SetParent(canvasTransform);//Canvas 위에 그릴 수 있게
        rectTransform.SetAsLastSibling();//맨 앞으로 보이게 설정

        //int selectedItemId = UnityEngine.Random.Range(0, items.Count);//랜덤한 수
        ItemSO weaponData = GetRandomWeaponItem();
        inventoryItem.Set(weaponData);//아이템 설정
    }
    private void CreateItemWithId(int id) //아이템 랜덤 생성
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>(); //빈 아이템 객체 생성
        selectedItem = inventoryItem;//선택한 아이템 설정

        rectTransform = inventoryItem.GetComponent<RectTransform>();//트랜스폼 가져옴
        rectTransform.SetParent(canvasTransform);//Canvas 위에 그릴 수 있게
        rectTransform.SetAsLastSibling();//맨 앞으로 보이게 설정

        //int selectedItemId = UnityEngine.Random.Range(0, items.Count);//랜덤한 수
        ItemSO weaponData = GetRandomWeaponItem(id);
        inventoryItem.Set(weaponData);//아이템 설정
    }
    public ItemSO GetRandomWeaponItem(int id)//랜덤한 아이템 반환 
    {
        WeaponData weaponData = DataBase.Weapon.Get(id);
        return weaponData;
    }
    public ItemSO GetRandomWeaponItem()//랜덤한 아이템 반환 
    {
        int selectedItemId = UnityEngine.Random.Range(0, DataBase.Weapon.GetItemIdCount());
        selectedItemId = DataBase.Weapon.GetItemId(selectedItemId);//랜덤으로 아이템 정보 가져오기
        WeaponData weaponData = DataBase.Weapon.Get(selectedItemId);
        return weaponData;
    }
    //public ItemSO GetRandomEquipmentItem()//변경점 랜덤한 아이템 반환 
    //{
    //    int selectedItemId = UnityEngine.Random.Range(0, DataBase..GetItemIdCount());
    //    selectedItemId = DataBase.Weapon.GetItemId(selectedItemId);//랜덤으로 아이템 정보 가져오기
    //    ItemSO weaponData = DataBase.Weapon.Get(selectedItemId);
    //    return weaponData;
    //}

    //public void LeftMouseButtonPress() //마우스 클릭했을 때 (원본)
    //{
    //    Vector2Int tileGridPosition = GetTileGridPosition();//Grid 좌표 가져옴

    //    if (selectedItem == null)//선택한 게 없다면
    //    {
    //        PickUpItem(tileGridPosition);//들고
    //    }
    //    else
    //    {
    //        PlaceItem(tileGridPosition);//설치한다.
    //    }
    //}
    public void LeftMouseButtonPress() //마우스 누른 순간
    {
        Vector2Int tileGridPosition = GetTileGridPosition();//마우스 위치의 Grid 좌표 가져옴
        PickUpItem(tileGridPosition);//마우스 위치의 아이템을 들고 selectedItem 설정
        startRotation = selectedItem.rotationDegree;//처음 회전 값 저장.
        SelectedItemGrid.AddCurrentCount(-1);
    }
    public void LeftMouseButtonPut()//마우스 뗀 순간
    {
        Vector2Int tileGridPosition = GetTileGridPosition();//마우스 위치의 grid 상 첫 칸의 좌표
        if (DragPlaceItem(tileGridPosition) == false)//설치할 수 없으면 selectedItem은 유지
        {
            ItemGrid temp = SelectedItemGrid;//마우스 위치의 Grid 임시 저장.
            SelectedItemGrid = previousItemGird;//이동 전 그리드로 재설정

            selectedItem.SetRotation(startRotation);//처음 회전 값으로 설정 width와 height에 영향을 주므로 먼저 실행되어야 한다.
            Vector2Int tileGridStartPosition = GetTileGridPosition(startPosition); //원래의 있던 곳의 위치

            PlaceItem(tileGridStartPosition);//시작 위치에 다시 설치
            SelectedItemGrid.AddCurrentCount(1);

            SelectedItemGrid = temp;//현재 선택 Grid를 마우스 위치의 Grid로 설정
        }
    }
    //private Vector2 GridToScreenPosition(Vector2Int gridPosition) //그리드 좌표를 스크린 좌표로 변환
    //{
    //    Vector2 screenPosition = SelectedItemGrid.GridToScreenPosition(gridPosition);
    //    return screenPosition;
    //}
    private Vector2Int GetTileGridPosition(Vector2 putPosition)//특정 좌표의 Grid상의 좌표를 얻는다. 아이템을 선택했다면 첫칸의 좌표
    {
        Vector2 position = putPosition; //마우스위치에는 물체의 중심이 온다.

        if (selectedItem != null) //선택 된 Grid가 있다면
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.TileSizeWidth / 2; //물체의 중심을 첫 번째 칸으로 이동.
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.TileSizeHeight / 2; //물체의 중심을 첫 번째 칸으로 이동.
        }
        return selectedItemGrid.GetTileGridPosition(position); //position의 스크린 상 좌표를 Grid상 좌표로 변환
    }
    private Vector2Int GetTileGridPosition() //마우스의 위치를 가지고 Grid상의 좌표를 얻는다. 아이템을 선택했다면 첫칸의 좌표
    {
        Vector2 position = Input.mousePosition; //마우스위치에는 물체의 중심이 온다.

        if (selectedItem != null) //선택 된 Grid가 있다면
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.TileSizeWidth / 2; //물체의 중심을 첫 번째 칸으로 이동.
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.TileSizeHeight / 2; //물체의 중심을 첫 번째 칸으로 이동.
        }
        return selectedItemGrid.GetTileGridPosition(position); //position의 스크린 상 좌표를 Grid상 좌표로 변환
    }
    private bool DragPlaceItem(Vector2Int tileGridPosition) //물체를 설치할 수 있는지 체크 후 설치
    {
        // 변경점.
        // 설치하려는 ItemGrid가 상점일 경우 false
        if(selectedItemGrid == storeGrid) return false;

        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapitem); //설치할 수 있으면 바로 설치
        if (complete) // 설치가 되었으면
        {
            TradeItem();
            selectedItem = null; //선택을 초기화
            if (overlapitem != null)//겹치는 것이 있었으면 
            {
                selectedItem = overlapitem; //겹친 아이템을 선택한 것으로 설정
                overlapitem = null;//겹친 아이템 null 초기화
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();//선택한 것을 맨 위로 보이게 함.
            }
        }
        return complete;
    }
    public void TradeItem()//다른 곳에 설치했을 때 아이템 이동
    {
        if (SelectedItemGrid != previousItemGird)//다른 곳에 설치했을 때
        {
            if (previousItemGird == playerInventoryGrid)//이동 전이 플레이어 인벤토리라면
            {
                previousItemGird.SubtractItemFromInventory(selectedItem);//인벤토리에서 아이템 빼기
            }
            if (selectedItemGrid == playerInventoryGrid)//이동 후가 플레이어 인벤토리라면
            {
                selectedItemGrid.AddItemToInventory(selectedItem);//인벤토리에 집어 넣기
            }
            // 변경점.
            // 상점 → 플레이어 인벤토리로 이동 시
            if(previousItemGird == storeGrid && selectedItemGrid == playerInventoryGrid)
            {
                // 상점에 저장된 아이템 GameObject 초기화
                for (int i = 0; i < storeGrid.currentStoreItem.Count; ++i)
                {
                    if (storeGrid.currentStoreItem[i] == selectedItem) storeGrid.currentStoreItem[i] = null;
                }
            }
            //previousItemGird.SubtractItemFromInventory(selectedItem); //원래 이거였던 것
            //selectedItemGrid.AddItemToInventory(selectedItem);
            //selectedItemGrid.AddCurrentCount(1);
        }
        //else//제자리서 들었다 놓았을 때 //원래 이거였던 것
        //{
        //    selectedItemGrid.AddCurrentCount(1);
        //}
        selectedItemGrid.AddCurrentCount(1);//현재 선택된 Grid의 Count를 증가 (Pick UP 할 때 -1을 하므로)
    }
    public void PlaceItem(Vector2Int tileGridPosition) //물체 설치
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y,ref overlapitem); //설치할 수 있으면 바로 설치
        if (complete) // 설치가 되었으면
        {
            selectedItem = null; //선택을 초기화
            if (overlapitem != null)//겹치는 것이 있었으면 
            {
                selectedItem = overlapitem; //겹친 아이템을 선택한 것으로 설정
                overlapitem = null;//겹친 아이템 null 초기화
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();//선택한 것을 맨 위로 보이게 함.
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition) //해당 좌표의 아이템을 선택한다. 있던 자리는 null로 변경
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y); // 선택한 아이템으로 설정
        previousItemGird = selectedItemGrid;//이전 그리드 설정
        //Debug.Log($"{tileGridPosition.x}, {tileGridPosition.y}");
        if (selectedItem != null)
        {
            Debug.Log($"현재 아이템 : {selectedItem.itemSO.Sprite.name}");
            rectTransform = selectedItem.GetComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform);//Canvas 위에 그릴 수 있게
            rectTransform.SetAsLastSibling();
        }
    }
    private void ItemIconDrag() //선택된 아이템의 위치를 마우스 위치로 이동
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
    public void PrintAllPlayerInventory()//플레이어가 가지고 있는 아이템 전체 목록 로그 찍기
    {
        int num = 0;
        foreach (var itemType in playerInventoryGrid.inventory)
        {
            foreach (var item in itemType.Value)
            {
                if (item != null)
                {
                    Debug.Log($"{itemType.Key} - {item.itemSO.Sprite.name}");
                    num++;
                }
            }
        }
        Debug.Log($"소지한 아이템 수 : {num}");
    }
    public void AddBigInventory() //실험용 넓은 판대기 적용
    {
        playerInventoryGrid.AddBigInventory();
    }
    public bool CheckInventoryToStorage(InventoryItem currentItem) //인벤토리에서 창고로 이동이 가능한지 확인
    {
        Vector2Int? storagePosition = storageGrid.FindSpaceForObject(currentItem);//창고에 자리가 있는지 확인
        if (storagePosition == null)
            return false;
        else
            return true;
    }
    public void MoveInventoryToStorage(InventoryItem currentItem) //인벤토리 아이템을 창고로 이동
    {
        Vector2Int itemPosition = GetTileGridPosition(new Vector2(currentItem.transform.position.x, currentItem.transform.position.y));
        PickUpItem(itemPosition);
        SelectedItemGrid.AddCurrentCount(-1);

        selectedItemGrid = storageGrid;
        Vector2Int? storagePosition = storageGrid.FindSpaceForObject(currentItem);
        PlaceItem(storagePosition.Value);
        SelectedItemGrid.AddCurrentCount(1);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 상점
    private void CreateRandomStoreItem() //아이템 랜덤 생성
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>(); //빈 아이템 객체 생성
        selectedItem = inventoryItem;//선택한 아이템 설정

        rectTransform = inventoryItem.GetComponent<RectTransform>();//트랜스폼 가져옴
        rectTransform.SetParent(canvasTransform);//Canvas 위에 그릴 수 있게
        rectTransform.SetAsLastSibling();//맨 앞으로 보이게 설정

        //int selectedItemId = UnityEngine.Random.Range(0, items.Count);//랜덤한 수
        //ItemSO weaponData = GetRandomStoreItem();
        ItemSO itemData = GetRandomStoreItem();
        inventoryItem.Set(itemData);//아이템 설정
    }

    public ItemSO GetRandomStoreItem()//랜덤한 아이템 반환 
    {
        int random = UnityEngine.Random.Range(0, 3);
        int selectedItemId;
        ItemSO itemData;

        switch (random)
        {
            case 0:
                selectedItemId = UnityEngine.Random.Range(0, DataBase.Consumable.GetItemIdCount());
                selectedItemId = DataBase.Consumable.GetItemId(selectedItemId); //랜덤으로 아이템 정보 가져오기
                itemData = DataBase.Consumable.Get(selectedItemId);
                break;
            case 1:
                selectedItemId = UnityEngine.Random.Range(0, DataBase.Equipments.GetItemIdCount());
                selectedItemId = DataBase.Equipments.GetItemId(selectedItemId);
                itemData = DataBase.Equipments.Get(selectedItemId);
                break;
            default:
                selectedItemId = UnityEngine.Random.Range(0, DataBase.Weapon.GetItemIdCount());
                selectedItemId = DataBase.Weapon.GetItemId(selectedItemId);
                itemData = DataBase.Weapon.Get(selectedItemId);
                break;
        }
        
        //WeaponData weaponData = DataBase.Weapon.Get(selectedItemId);
        return itemData;
    }

    private void InsertRandomStoreItem()//아이템 랜덤 생성 후 배치
    {
        selectedItemGrid = storeGrid;

        CreateRandomStoreItem();//아이템 생성
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertStoreItem(itemToInsert);
    }

    private void InsertStoreItem(InventoryItem itemToInsert)//상점에 아이템 배치
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null)//설치 불가능 이면 return
        {
            Destroy(itemToInsert.gameObject);
            Debug.Log("삭제완료");
            return;
        }

        //selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
        // ItemData에 Item 가격 추가하는 것 건의하기.
        //_itemCost.text = itemToInsert.itemSO.Price.ToString();

        storeGrid.AddStoreStock(itemToInsert);
    }

    public void OnStoreReroll()
    {
        RemoveStoreStock();

        for (int i = 0; i < 5; ++i)
            InsertRandomStoreItem();

        storeGrid.ClearEmptySolts();
    }

    public void RemoveStoreStock()
    {
        if (storeGrid.currentStoreItem == null) return;

        storeGrid.ResetPanelStates();
    }
    
    public void OnClickNextStageButton()
    { 
        nextStage();
    }

    public void AddStartWeapon(ItemSO item)
    {
        selectedItemGrid = playerInventoryGrid;

        CreateItemWithId(item.Id);
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    internal void SellItemButton(InventoryItem currentItem)
    {
        Vector2Int gridPosition = new Vector2Int(currentItem.onGridPositionX, currentItem.onGridPositionY);
        PickUpItem(gridPosition);
        //Vector2Int tileGridPosition = GetTileGridPosition();//마우스 위치의 Grid 좌표 가져옴
        //PickUpItem(tileGridPosition);
        if (selectedItem == null) 
            return;
        playerInventoryGrid.SubtractItemFromInventory(selectedItem);//아이템 없애고
        Destroy(selectedItem.gameObject);
        selectedItem = null;
        SelectedItemGrid.AddCurrentCount(-1);

        ItemDescription itemDescription = itemDescriptionUI.GetComponent<ItemDescription>();

        if(itemDescription != null)
            itemDescription.ExitExplnationUI();
    }

}
