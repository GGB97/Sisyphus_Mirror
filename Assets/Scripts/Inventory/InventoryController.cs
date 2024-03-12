using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid { 
        get => selectedItemGrid; 
        set { 
            selectedItemGrid = value;
            inventoryHighlight.SetParent(selectedItemGrid);
        } 
    }

    InventoryItem selectedItem;
    InventoryItem overlapitem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;
    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }
    private void Update()
    {
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedItem == null)
            { 
                CreateRandomItem(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            InsertRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        HandleHightlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    private void InsertRandomItem()
    {
        if (selectedItemGrid == null) { return; }

        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null)
        {
            return;
        }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    InventoryItem itemToHighlight;

    private void HandleHightlight()//하이라이트 표시하기
    {
        Vector2Int positionOnGrid = GetTileGridPosition();//Grid 상 마우스 위치 좌표
        if (selectedItem == null) //선택한 것이 아무것도 없으면
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x , positionOnGrid.y);// x, y에 해당하는 아이템을 가져옴
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
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, selectedItem.itemData.width, selectedItem.itemData.height));//활성화
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
        rectTransform.SetAsLastSibling();

        int selectedItemId = UnityEngine.Random.Range(0, items.Count);//랜덤한 수
        inventoryItem.Set(items[selectedItemId]);//아이템 설정
    }

    private void LeftMouseButtonPress() //마우스 클릭했을 때
    {
        Vector2Int tileGridPosition = GetTileGridPosition();//Grid 좌표 가져옴

        if (selectedItem == null)//선택한 게 없다면
        {
            PickUpItem(tileGridPosition);//들고
        }
        else
        {
            PlaceItem(tileGridPosition);//설치한다.
        }
    }

    private Vector2Int GetTileGridPosition() //Grid상의 좌표를 얻는다.
    {
        Vector2 position = Input.mousePosition; //마우스위치에는 물체의 중심이 온다.

        if (selectedItem != null) //선택 된 Grid가 있다면
        {
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.TileSizeWidth / 2; //물체의 중심을 첫 번째 칸으로 이동.
            position.y += (selectedItem.itemData.height - 1) * ItemGrid.TileSizeHeight / 2; //물체의 중심을 첫 번째 칸으로 이동.
        }
        return selectedItemGrid.GetTileGridPosition(position); //position의 스크린 상 좌표를 Grid상 좌표로 변환
    }

    private void PlaceItem(Vector2Int tileGridPosition) //물체 설치
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y,ref overlapitem);
        if (complete)
        {
            selectedItem = null;
            if (overlapitem != null)
            {
                selectedItem = overlapitem;
                overlapitem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        Debug.Log("null");
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
            //rectTransform.SetAsLastSibling();
        }
    }
    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
