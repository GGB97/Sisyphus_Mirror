using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHighlight : MonoBehaviour
{
    RectTransform highlighter;
    Image image;
    private void Awake()
    {
        highlighter = GetComponent<RectTransform>();
        image = GetComponent<Image>();

    }
    public void Show(bool b) //매개변수로 하이라이트를 껐다 킴
    {
        highlighter.gameObject.SetActive(b);
    }
    public void SetSize(InventoryItem targetItem)//하이라이트의 사이즈를 아이템의 사이즈로 설정
    {
        Vector2 size = new Vector2();
        size.x = targetItem.WIDTH * ItemGrid.TileSizeWidth;
        size.y = targetItem.HEIGHT * ItemGrid.TileSizeHeight;
        highlighter.sizeDelta = size;
    }
    public void SetOriginSize(InventoryItem targetItem)//하이라이트의 사이즈를 아이템의 사이즈로 설정
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemSO.IconWidth * ItemGrid.TileSizeWidth;
        size.y = targetItem.itemSO.IconHeight * ItemGrid.TileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetitem)//하이라이트의 위치를 아이템이 놓일 자리에 설정
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetitem, targetitem.onGridPositionX, targetitem.onGridPositionY);
        highlighter.localPosition = pos;
    }
    public void SetRotation(InventoryItem targetItem)
    {
        highlighter.rotation = Quaternion.Euler(0, 0, targetItem.rotationDegree);
    }
    public void SetParent(ItemGrid targetGrid)//그리드 위에 그려질 수 있도록 설정
    {
        if (targetGrid == null)
        {
            return;
        }
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetitem,int posX,int posY) //
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetitem,posX,posY);
        highlighter.localPosition = pos;
    }
    public void SetImage(InventoryItem inventoryItem)
    {
        image.sprite = inventoryItem.itemSO.Sprite;
    }
}
