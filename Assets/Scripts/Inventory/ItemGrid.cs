using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TileSizeWidth = 32f;//가로 타일의 사이즈
    public const float TileSizeHeight = 32f;//세로 타일의 사이즈

    InventoryItem[,] inventoryItemSlot;

    public InventoryPanel inventoryPanel; // 한 칸들의 모음 판넬

    RectTransform rectTransform;//그리드의 트랜스 폼

    [SerializeField] int gridSizeWidth = 10;//가로 길이
    [SerializeField] int gridSizeHeight = 10; // 세로 길이

    Vector2 mousePositionOnTheGrid; //그리드 왼쪽 상단에서 마우스의 위치 값
    Vector2Int tileGridPosition = new Vector2Int(); //그리드 위에서의 좌표
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);// 가로, 세로 길이만큼 grid 칸 생성
    }

    private void Init(int width, int height)//그리드 초기 생성
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * TileSizeWidth,height * TileSizeHeight);
        rectTransform.sizeDelta = size;

        inventoryPanel = GetComponentInChildren<InventoryPanel>();//판넬 가져옴

        if(inventoryPanel != null)
            inventoryPanel.Init(width, height);//판넬 초기화
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition) //그리드 판의 왼쪽 상단을 0,0 오른쪽 하단을 max,max로하는 그리드 위에서의 좌표반환
    {
        mousePositionOnTheGrid.x = mousePosition.x - rectTransform.position.x;//Grid 와 마우스의 떨어짐 정도
        mousePositionOnTheGrid.y = rectTransform.position.y - mousePosition.y;//Grid 와 마우스의 떨어짐 정도

        tileGridPosition.x = (int)(mousePositionOnTheGrid.x / TileSizeWidth);//Grid 상 좌표로 변환
        tileGridPosition.y = (int)(mousePositionOnTheGrid.y / TileSizeHeight);//Grid 상 좌표로 변환

        return tileGridPosition;
    }
    public bool PlaceItem(InventoryItem inventoryItem,int posX,int posY, ref InventoryItem overlapitem) //그리드 좌표 x,y에 아이템 배치
    {
        if (BoundryCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height) == false) //아이템이 Grid 안에 있는지 체크 
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height, ref overlapitem) == false)//물체 마지막 하단 칸에 설치할 수 있는지 체크
        {
            overlapitem = null; //겹치는 것이 있으면 overlap 초기화
            return false; //실패 반환
        }

        if (overlapitem != null) //겹치는 것이 있다면
        {
            CleanGridReference(overlapitem);//겹치는 곳 null로 만듬
        }

        PlaceItem(inventoryItem, posX, posY);//새로운 아이템 놓기

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform); //현재 그리드를 자신의 부모로 설정

        for (int x = 0; x < inventoryItem.itemData.width; x++) // 아이템 배열에 아이템 크기에 해당하는 칸 수만큼 저장.
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
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
        position.x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.itemData.width / 2;//자신의 중심을 표현
        position.y = -(posY * TileSizeHeight + TileSizeHeight * inventoryItem.itemData.height / 2);//자신의 중심을 표현
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
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
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

        CleanGridReference(toReturn);

        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)//아이템의 정보로 차지하는 공간만큼 초기화
    {
        for (int ix = 0; ix < item.itemData.width; ix++)
        {
            for (int iy = 0; iy < item.itemData.height; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;//아이템 공간만큼 null
            }
        }
    }

    bool PositionCheck(int posX, int posY) //grid 안에 있는지 확인 후 bool 값 리턴
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

    public bool BoundryCheck(int posX, int posY, int width, int height)//Grid 안에 물체가 포함되는지 확인
    {
        if (PositionCheck(posX, posY) == false) //물체의 첫 칸의 위치가 grid안에 없다면
        {
            return false;
        }

        posX += width - 1;
        posY += height - 1;

        if (PositionCheck(posX, posY) == false) //물체의 우하단 위치가 grid안에 없다면
        {
            return false;
        }

        return true;
    }

    public InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.itemData.height +1;
        int width = gridSizeWidth - itemToInsert.itemData.width +1;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                if (CheckAvailableSpace(x, y, itemToInsert.itemData.width, itemToInsert.itemData.height) == true)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }
}
