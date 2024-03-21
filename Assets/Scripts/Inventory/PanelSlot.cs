using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PanelSlotState
{
    Empty,
    Full,
    Add,
    Null,
}
public class PanelSlot : MonoBehaviour , IPointerClickHandler
{
    private InventoryController inventoryController;
    public PanelSlotState state = PanelSlotState.Null;
    Image image;
    public int posX;
    public int posY;
    private void Awake()
    {
        image = GetComponent<Image>();
        inventoryController = InventoryController.Instance;
    }
    private void Start()
    {
        
    }
    public void ChangeSlotState(PanelSlotState changeState)//slot의 상태 변경하기
    {
        state = changeState;//상태 변경
        ChangeSlotSprite((int)changeState);
    }
    private void ChangeSlotSprite(int num)//상태에 맞는 스프라이트 변경
    {
        if ((PanelSlotState)num == PanelSlotState.Full)//바닥을 채워야 하면
        {
            ItemGrade itemGrade = inventoryController.SelectedItemGrid.inventoryItemSlot[posX, posY].itemData.itemGrade;
            ChangeBlockColor(itemGrade);
        }
        else
        {
            ResetBlockColor();
        }
        image.sprite = inventoryController.slotSprites[num];
    }
    public void ChangeBlockColor(ItemGrade itemGrade)
    {
        image.color = inventoryController.BlockColorDictionary[itemGrade].color;
    }
    public void ResetBlockColor()
    {
        image.color = Color.white;
    }
    public bool CompareState(PanelSlotState compareState) //상태 비교하기
    {
        return state == compareState ? true : false;
    }
    public void SetPosition(int x, int y) // 자신의 Grid 좌표
    {
        posX = x;
        posY = y;
    }
    public void OnPointerClick(PointerEventData eventData) // 클릭했을 때
    {
        if (state == PanelSlotState.Add) //자신이 Add 상태일 때만 추가한다.
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                InventoryController.Instance.playerInventoryGrid.CreateAddSlot();
            }
        }
    }
}
