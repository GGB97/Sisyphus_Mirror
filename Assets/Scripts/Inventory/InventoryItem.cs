using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemSO itemSO;//아이템 가로,세로,스프라이트 정보
    public int HEIGHT
    {
        get
        {
            if (((int)rotationDegree / 90) % 2 == 1) //90 , 270
            {
                return itemSO.IconWidth;
            }
            else
            {
                return itemSO.IconHeight;
            }
        }
    }
    public int WIDTH
    {
        get
        {
            if (((int)rotationDegree / 90) % 2 == 1) //90 , 270
            {
                return itemSO.IconHeight;
            }
            else
            {
                return itemSO.IconWidth;
            }
        }
    }

    public int onGridPositionX;//아이템 좌상단 첫 칸의 X정보
    public int onGridPositionY;//아이템 좌상단 첫 칸의 Y정보

    public float rotationDegree = 0; //회전 정도

    public void Set(ItemSO itemSO) //아이템 생성할 때 아이템 설정
    {
        this.itemSO = itemSO;

        //ItemData itemData = new ItemData();

        //itemData.width = weaponData.IconWidth;
        //itemData.height = weaponData.IconHeight;
        //itemData.itemIcon = weaponData.Sprite;
        //itemData.id = weaponData.Id;
        //itemData.itemType = weaponData.ItemType;
        //itemData.itemGrade = weaponData.Grade;

        //this.itemSO = itemData; //아이템 데이터를 매개변수로 설정

        GetComponent<Image>().sprite = itemSO.Sprite; //이미지 변경

        Vector2 size = new Vector2();
        size.x = itemSO.IconWidth * ItemGrid.TileSizeWidth; //아이템의 가로 길이
        size.y = itemSO.IconHeight * ItemGrid.TileSizeHeight;//아이템의 세로 길이
        GetComponent<RectTransform>().sizeDelta = size;//아이템 사이즈 설정
        rotationDegree = GetComponent<RectTransform>().rotation.z;
    }

    public void Rotate()//90도 회전
    {
        rotationDegree += 90f;//90도 더한다.
        if (rotationDegree >= 360f)//360도를 넘으면 초기화
            rotationDegree -= 360f;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotationDegree);//회전 적용
    }
    public void SetRotation(float degree)//degree로 로테이션 설정
    {
        rotationDegree = degree;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotationDegree);
    }
}
