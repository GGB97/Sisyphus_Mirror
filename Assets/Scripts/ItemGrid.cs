using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float TileSizeWidth = 32f;//가로 타일의 사이즈
    const float TileSizeHeight = 32f;//세로 타일의 사이즈

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;//그리드의 트랜스 폼

    [SerializeField] int gridSizeWidth = 10;//가로 길이
    [SerializeField] int gridSizeHeight = 10; // 세로 길이

    [SerializeField] GameObject inventoryItemPrefab;

    Vector2 mousePositionOnTheGrid; //그리드 왼쪽 상단에서 마우스의 위치 값
    Vector2Int tileGridPosition = new Vector2Int(); //그리드 위에서의 좌표
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);

        
    }
    void Start()
    {
        InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 4, 4);
        
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
    public void PlaceItem(InventoryItem inventoryItem,int posX,int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        inventoryItemSlot[posX, posY] = inventoryItem;

        Vector2 position = new Vector2();
        position.x = (posX * TileSizeWidth) + (TileSizeWidth / 2);
        position.y = -((posY * TileSizeHeight) + (TileSizeHeight / 2));

        rectTransform.localPosition = position; //지역 위치를 position값으로
    }
}
