using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TileSizeWidth = 32f;//가로 타일의 사이즈
    public const float TileSizeHeight = 32f;//세로 타일의 사이즈

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;//그리드의 트랜스 폼

    [SerializeField] int gridSizeWidth = 10;//가로 길이
    [SerializeField] int gridSizeHeight = 10; // 세로 길이

    Vector2 mousePositionOnTheGrid; //그리드 왼쪽 상단에서 마우스의 위치 값
    Vector2Int tileGridPosition = new Vector2Int(); //그리드 위에서의 좌표
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);// 가로, 세로 길이만큼 grid 칸 생성
    }

    private void Init(int width, int height)//그리드 초기 생성
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * TileSizeWidth,height * TileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition) //그리드 왼쪽 상단을 0,0 오른쪽 하단을 max,max로하는 그리드 위에서의 좌표반환
    {
        mousePositionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        mousePositionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(mousePositionOnTheGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(mousePositionOnTheGrid.y / TileSizeHeight);

        return tileGridPosition;
    }
    public bool PlaceItem(InventoryItem inventoryItem,int posX,int posY) //그리드 좌표 x,y에 아이템 배치
    {
        if (BoundryCheck(posX, posX, inventoryItem.itemData.width, inventoryItem.itemData.height) == false)
        { 
            return false; 
        }

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.itemData.width; x++) // 아이템 배열에 아이템 크기에 해당하는 칸 수만큼 저장.
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX; //자신의 기준인 posX를 저장
        inventoryItem.onGridPositionY = posY; //자신의 기준인 posY를 저장

        Vector2 position = new Vector2();
        position.x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.itemData.width / 2;
        position.y = -((posY * TileSizeHeight) + (TileSizeHeight * inventoryItem .itemData.height / 2));

        rectTransform.localPosition = position; //지역 위치를 position값으로

        return true;
    }

    public InventoryItem PickUpItem(int x, int y)//x,y에 해당하는 아이템을 반환 있던 자리는 null
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; } //빈 공간을 터치했을 때 널 리턴

        for (int ix = 0; ix < toReturn.itemData.width; ix++)
        {
            for (int iy = 0; iy < toReturn.itemData.height; iy++)
            {
                inventoryItemSlot[toReturn.onGridPositionX + ix, toReturn.onGridPositionY + iy] = null;
            }
        }
        return toReturn;
    }

    bool PositionCheck(int posX, int posY) //grid 안에 있는지 확인 후 bool 값 리턴
    {
        if (posX < 0 || posY > 0)
        {
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if (PositionCheck(posX, posY) == false) //처음 위치가 grid안에 없다면
        {
            return false;
        }

        posX += width - 1;
        posY += height - 1;
        if (PositionCheck(posX, posY) == false) //우하단 위치가 grid안에 없다면
        {
            return false;
        }
        return true;
    }
}
