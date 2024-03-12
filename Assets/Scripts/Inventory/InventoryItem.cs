using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;//아이템 정보

    public int HEIGHT
    {
        get
        {
            if (rotated == false)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }
    public int WIDTH
    {
        get
        {
            if (rotated == false)
            {
                return itemData.width;
            }
            return itemData.height;
        }
    }

    public int onGridPositionX;//아이템 첫 칸의 X정보
    public int onGridPositionY;//아이템 첫 칸의 Y정보

    public bool rotated = false;

    public void Set(ItemData itemData) //아이템 설정
    {
        this.itemData = itemData; //아이템 데이터를 매개변수로 설정

        GetComponent<Image>().sprite = itemData.itemIcon; //이미지 변경

        Vector2 size = new Vector2();
        size.x = itemData.width * ItemGrid.TileSizeWidth; //아이템의 가로 길이
        size.y = itemData.height * ItemGrid.TileSizeHeight;//아이템의 세로 길이
        GetComponent<RectTransform>().sizeDelta = size;//아이템 사이즈 설정
    }

    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? 90f : 0f);
    }
}
