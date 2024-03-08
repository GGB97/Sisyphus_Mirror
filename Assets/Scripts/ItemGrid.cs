using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float TileSizeWidth = 32f;
    const float TileSizeHeight = 32f;

    RectTransform rectTransform;

    Vector2 mousePositionOnTheGrid; //그리드 왼쪽 상단에서 마우스의 위치 값
    Vector2Int tileGridPosition = new Vector2Int(); //그리드 위에서의 좌표
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public Vector2Int GetTileGridPosition(Vector2 mousePosition) //그리드 왼쪽 상단을 0,0 오른쪽 하단을 max,max로하는 그리드 위에서의 좌표반환
    {
        mousePositionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        mousePositionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        Vector2Int tileGridPosition = new Vector2Int();
        tileGridPosition.x = (int)(mousePositionOnTheGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(mousePositionOnTheGrid.y / TileSizeHeight);

        return tileGridPosition;
    }
    private void Update()
    {
        tileGridPosition = GetTileGridPosition(Input.mousePosition);
        Debug.Log($"{tileGridPosition}");
    }
}
