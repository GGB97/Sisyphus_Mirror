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
    public PanelSlotState state = PanelSlotState.Null;
    //public RectTransform rectTransform;
    Image image;
    public int posX;
    public int posY;
    private void Awake()
    {
        image = GetComponent<Image>();
        //rectTransform = GetComponent<RectTransform>();
    }
    public void ChangeSlotState(PanelSlotState changeState)
    {
        state = changeState;//상태 변경
        ChangeSlotSprite((int)changeState);
        //ChangeSlotSprite();
    }
    //private void ChangeSlotSprite()//상태에 맞는 스프라이트 변경
    //{
    //    switch (state) 
    //    {
    //        case PanelSlotState.Empty:
    //            image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Empty];
    //            break;
    //        case PanelSlotState.Full:
    //            image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Full];
    //            break;
    //        case PanelSlotState.Add:
    //            image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Add];
    //            break;
    //        case PanelSlotState.Null:
    //            image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Null];
    //            break;
    //        default:
    //            {
    //                image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Null];
    //                break;
    //            }
    //    }
    //}
    private void ChangeSlotSprite(int num)//상태에 맞는 스프라이트 변경
    {
        image.sprite = InventoryController.Instance.slotSprites[num];
    }
    public bool CompareState(PanelSlotState compareState)
    {
        return state == compareState ? true : false;
    }
    public void SetPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (state == PanelSlotState.Add) //자신이 Add 상태일 때만 추가한다.
        {
            InventoryController.Instance.SelectedItemGrid.CreateAddSlot(); //그
        }
    }
}
